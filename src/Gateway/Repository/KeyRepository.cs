using Gateway.Data;
using Gateway.Models.Entities;

namespace Gateway.Repository;

public class KeyRepository : RepositoryBase, IKeyRepository
{
    public KeyRepository(HowToLearnDbContext context) : base(context) { }

    public async Task<Key> GetKeyAsync(Guid id)
        => await this.GetOperationAsync<Key>(id).ConfigureAwait(false);

    public async Task<IEnumerable<Key>> GetKeysAsync()
        => await this.GetOperationAsync<Key>().ConfigureAwait(false);

    public async Task AddKeyAsync(Key key)
        => await this.AddOperationAsync(key).ConfigureAwait(false);

    public async Task RemoveKeyAsync(Guid id)
        => await this.RemoveOperationAsync<Key>(id).ConfigureAwait(false);

    public async Task UpdateKeyAsync(Key updatedKey)
        => await this.UpdateOperationAsync(updatedKey).ConfigureAwait(false);
}