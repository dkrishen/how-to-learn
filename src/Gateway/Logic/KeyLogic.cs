using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;
using Gateway.Repository;

namespace Gateway.Logic;

public class KeyLogic : LogicCrud<Key, KeyViewDto, KeyPostDto, KeyUpdateDto>, IKeyLogic
{
    private readonly IKeyRepository _keyRepository;
    private readonly ITopicRepository _topicRepository;
    private readonly IMapper _mapper;

    public KeyLogic(IKeyRepository keyRepository, ITopicLogic topicLogic, IMapper mapper)
        :base(keyRepository, mapper)
    {
        _keyRepository = keyRepository;
        _mapper = mapper;
    }

    public override async Task<Guid> AddAsync(KeyPostDto key)
    {
        var topicId = await _topicRepository.GetTopicIdByTitleAsync(key.Topic).ConfigureAwait(false);
        var newKey = _mapper.Map<Key>(key);
        newKey.Reference = topicId;
        return await _keyRepository.AddAsync(newKey).ConfigureAwait(false);
    }

    public override async Task<KeyViewDto> GetAsync(Guid id)
    {
        var key = await _keyRepository.GetAsync(id).ConfigureAwait(false);
        var keyView = _mapper.Map<KeyViewDto>(key);
        var topic = await _topicRepository.GetAsync(key.Reference).ConfigureAwait(false);
        keyView.Topic = topic.Title;

        return keyView;
    }

    public override async Task<IEnumerable<KeyViewDto>> GetAsync()
    {
        var keys = await _keyRepository.GetAsync().ConfigureAwait(false);
        var keyViews = _mapper.Map<IEnumerable<KeyViewDto>>(keys);
        var topics = await _topicRepository.GetAsync().ConfigureAwait(false);

        foreach(var key in keyViews)
        {
            key.Topic = topics.FirstOrDefault(t => t.Id == Guid.Parse(key.Topic)).Title;
        }

        return keyViews;
    }

    public override async Task UpdateAsync(KeyUpdateDto key)
    {
        var topicId = await _topicRepository.GetTopicIdByTitleAsync(key.Topic).ConfigureAwait(false);
        var newKey = _mapper.Map<Key>(key);
        newKey.Reference = topicId;
        await _keyRepository.UpdateAsync(newKey).ConfigureAwait(false);
    }
}
