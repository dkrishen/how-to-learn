using Gateway.Core.Models;

namespace Gateway.Logic;

public interface ILogicCrud<TView, TPost, TUpdate> 
    where TView : class 
    where TPost : class 
    where TUpdate : class
{
    public Task<TView> GetAsync(Guid id);
    public Task<DataWithSlicePagination<TView>> GetAsync(Queries? options);
    public Task<Guid> AddAsync(TPost obj);
    public Task RemoveAsync(Guid id);
    public Task UpdateAsync(TUpdate obj);
}