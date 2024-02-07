using Gateway.Core.Models;
using Gateway.Data;

namespace Gateway.Repository;

public class RepositoryCrud<T> : RepositoryBase, IRepositoryCrud<T> where T : class, IIdentifiedObject
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
}
