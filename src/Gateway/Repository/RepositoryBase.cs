using Gateway.Core.Models;
using Gateway.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gateway.Repository
{
    public class RepositoryBase
    {
        protected HowToLearnDbContext context;
        public RepositoryBase(HowToLearnDbContext context)
            => this.context = context;

        protected async Task<T> GetOperationAsync<T>(Guid id) where T : class, IIdentifiedObject
            => await GetDbSet<T>()
                .SingleAsync(s => s.Id == id)
                .ConfigureAwait(false);

        protected async Task<IEnumerable<T>> GetOperationAsync<T>() where T : class, IIdentifiedObject
            => await GetDbSet<T>()
                .ToListAsync()
                .ConfigureAwait(false);

        protected async Task AddOperationAsync<T>(T newObj) where T : class, IIdentifiedObject
        {
            await GetDbSet<T>()
                .AddAsync(newObj)
                .ConfigureAwait(false);

            await context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        protected async Task RemoveOperationAsync<T>(Guid id) where T : class, IIdentifiedObject
        { 
            var table = GetDbSet<T>();

            var obj = await table
                .SingleAsync(s 
                    => s.Id == id)
                .ConfigureAwait(false);

            table.Remove(obj);

            await context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        protected async Task UpdateOperationAsync<T>(T updatedObj) where T : class, IIdentifiedObject
        {
            var fields = typeof(T)
                .GetProperties()
                .Where(p => !p.GetGetMethod().IsVirtual);

            var obj = await GetDbSet<T>()
                .SingleAsync(s
                    => s.Id == updatedObj.Id)
                .ConfigureAwait(false);

            foreach (var fld in fields)
            {
                fld.SetValue(obj, fld.GetValue(updatedObj));
            }

            await context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        private DbSet<T> GetDbSet<T>() where T : class, IIdentifiedObject
        {
            Type setType = typeof(DbSet<>).MakeGenericType(typeof(T));
            PropertyInfo setProperty = context.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType == setType);
            if (setProperty == null)
                throw new ArgumentException($"DbSet<{typeof(T)}> not found in context.");
            return (DbSet<T>)setProperty.GetValue(context);
        }
    }
}