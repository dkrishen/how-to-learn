using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic
{
    public interface IKeyLogic
    {
        public Task<KeyViewDto> GetKeyAsync(Guid id);
        public Task<IEnumerable<KeyViewDto>> GetKeysAsync();
        public Task AddKeyAsync(KeyPostDto Key);
        public Task UpdateKeyAsync(KeyUpdateDto Key);
        public Task DeleteKeyAsync(Guid id);
    }
}