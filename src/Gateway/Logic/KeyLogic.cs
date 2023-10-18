using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;
using Gateway.Repository;

namespace Gateway.Logic;

public class KeyLogic : IKeyLogic
{
    private readonly IKeyRepository _keyRepository;
    private readonly IMapper _mapper;

    public KeyLogic(IKeyRepository keyRepository, IMapper mapper)
    {
        _keyRepository = keyRepository;
        _mapper = mapper;
    }

    public async Task<KeyViewDto> GetKeyAsync(Guid id)
        => _mapper.Map<KeyViewDto>(
            await _keyRepository.GetKeyAsync(id).ConfigureAwait(false));

    public async Task<IEnumerable<KeyViewDto>> GetKeysAsync()
        => _mapper.Map<IEnumerable<KeyViewDto>>(
            await _keyRepository.GetKeysAsync().ConfigureAwait(false));

    public async Task AddKeyAsync(KeyPostDto key)
        => await _keyRepository.AddKeyAsync(
            _mapper.Map<Key>(key)).ConfigureAwait(false);

    public async Task DeleteKeyAsync(Guid id)
        => await _keyRepository.RemoveKeyAsync(id).ConfigureAwait(false);

    public async Task UpdateKeyAsync(KeyUpdateDto key)
        => await _keyRepository.UpdateKeyAsync(
            _mapper.Map<Key>(key)).ConfigureAwait(false);
}
