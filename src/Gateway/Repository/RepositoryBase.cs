using Gateway.Core.Models;
using Gateway.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gateway.Repository;

public class RepositoryBase
{
    protected HowToLearnDbContext context;
    public RepositoryBase(HowToLearnDbContext context)
        => this.context = context;

    protected DbSet<T> GetDbSet<T>() where T : class, IIdentifiedObject
    {
        Type setType = typeof(DbSet<>).MakeGenericType(typeof(T));
        PropertyInfo setProperty = context.GetType().GetProperties()
            .FirstOrDefault(p => p.PropertyType == setType);
        if (setProperty == null)
            throw new ArgumentException($"DbSet<{typeof(T)}> not found in context.");
        return (DbSet<T>)setProperty.GetValue(context);
    }
}