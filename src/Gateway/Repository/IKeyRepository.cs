using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface IKeyRepository
{
    public Task<Key> GetKeyAsync(Guid id);
    public Task<IEnumerable<Key>> GetKeysAsync();
    public Task<Guid> AddKeyAsync(Key key);
    public Task RemoveKeyAsync(Guid id);
    public Task UpdateKeyAsync(Key section);
}
