using Gateway.Core.Models;

namespace Gateway.Repository;

public interface IRepositoryCrud<T>
{
    public Task<T> GetAsync(Guid id);
    public Task<IEnumerable<T>> GetAsync();
    public Task<IEnumerable<T>> GetAsync(int page, int pageSize);
    public Task<IEnumerable<T>> GetAsync(string pattern);
    public Task<int> GetCountAsync();
    public Task<Guid> AddAsync(T obj);
    public Task RemoveAsync(Guid id);
    public Task UpdateAsync(T obj);
}