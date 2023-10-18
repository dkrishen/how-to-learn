using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;
using Gateway.Repository;

namespace Gateway.Logic;

public class TopicLogic : ITopicLogic
{
    private readonly ITopicRepository _topicRepository;
    private readonly IMapper _mapper;

    public TopicLogic(ITopicRepository topicRepository, IMapper mapper)
    {
        _topicRepository = topicRepository;
        _mapper = mapper;
    }

    public async Task<TopicViewDto> GetTopicAsync(Guid id)
        => _mapper.Map<TopicViewDto>(
            await _topicRepository.GetTopicAsync(id).ConfigureAwait(false));

    public async Task<IEnumerable<TopicViewDto>> GetTopicsAsync()
        => _mapper.Map<IEnumerable<TopicViewDto>>(
            await _topicRepository.GetTopicsAsync().ConfigureAwait(false));

    public async Task AddTopicAsync(TopicPostDto topic)
        => await _topicRepository.AddTopicAsync(
            _mapper.Map<Topic>(topic)).ConfigureAwait(false);

    public async Task DeleteTopicAsync(Guid id)
        => await _topicRepository.RemoveTopicAsync(id).ConfigureAwait(false);

    public async Task UpdateTopicAsync(TopicUpdateDto topic)
        => await _topicRepository.UpdateTopicAsync(
            _mapper.Map<Topic>(topic)).ConfigureAwait(false);
}
