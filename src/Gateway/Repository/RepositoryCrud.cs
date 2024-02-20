using Gateway.Core.Models;
using Gateway.Data;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Repository;

public abstract class RepositoryCrud<T> : RepositoryBase, IRepositoryCrud<T> where T : class, IIdentifiedObject
{
    public RepositoryCrud(HowToLearnDbContext context) : base(context)
    {
    }

    public virtual async Task<T> GetAsync(Guid id)
    => await this.GetOperationAsync<T>(id).ConfigureAwait(false);

    public virtual async Task<IEnumerable<T>> GetAsync()
        => await this.GetOperationAsync<T>().ConfigureAwait(false);

    public virtual async Task<Guid> AddAsync(T obj)
        => await this.AddOperationAsync(obj).ConfigureAwait(false);

    public virtual async Task RemoveAsync(Guid id)
        => await RemoveOperationAsync<T>(id).ConfigureAwait(false);

    public virtual async Task UpdateAsync(T obj)
        => await this.UpdateOperationAsync(obj).ConfigureAwait(false);

    public virtual async Task<IEnumerable<T>> GetAsync(int page, int pageSize)
        => await this.GetOperationAsync<T>(page, pageSize).ConfigureAwait(false);

    public virtual async Task<IEnumerable<T>> GetAsync(string pattern)
        => await this.GetOperationAsync<T>(pattern).ConfigureAwait(false);

    public virtual async Task<int> GetCountAsync()
        => await this.GetCountOperationAsync<T>().ConfigureAwait(false);


    protected async Task<T> GetOperationAsync<T>(Guid id) where T : class, IIdentifiedObject
        => await GetDbSet<T>()
            .SingleAsync(s => s.Id == id)
            .ConfigureAwait(false);

    protected async Task<IEnumerable<T>> GetOperationAsync<T>() where T : class, IIdentifiedObject
        => await GetDbSet<T>()
            .ToListAsync()
            .ConfigureAwait(false);

    protected abstract Task<IEnumerable<T>> GetOperationAsync<T>(string pattern) where T : class, IIdentifiedObject;

    protected async Task<IEnumerable<T>> GetOperationAsync<T>(int page, int pageSize) where T : class, IIdentifiedObject
        => await GetDbSet<T>()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync()
            .ConfigureAwait(false);

    protected async Task<int> GetCountOperationAsync<T>() where T : class, IIdentifiedObject
        => await GetDbSet<T>()
            .CountAsync()
            .ConfigureAwait(false);

    protected async Task<Guid> AddOperationAsync<T>(T newObj) where T : class, IIdentifiedObject
    {
        await GetDbSet<T>()
            .AddAsync(newObj)
            .ConfigureAwait(false);

        await context
            .SaveChangesAsync()
            .ConfigureAwait(false);

        return newObj.Id;
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
}
