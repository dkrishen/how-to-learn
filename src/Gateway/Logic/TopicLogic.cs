using AutoMapper;
using Gateway.Models.Elastic;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;
using Gateway.Repository;

namespace Gateway.Logic;

public class TopicLogic : LogicCrud<Topic, TopicViewDto, TopicPostDto, TopicUpdateDto>, ITopicLogic
{
    private readonly ITopicRepository _topicRepository;
    private readonly IElasticRepository _elasticRepository;
    private readonly IMapper _mapper;

    public TopicLogic(ITopicRepository topicRepository, IMapper mapper, IElasticRepository elasticRepository)
        : base(topicRepository, mapper)
    {
        _topicRepository = topicRepository;
        _elasticRepository = elasticRepository;
        _mapper = mapper;
    }

    public async Task<TopicViewDto[]> GetTopicsBySectionAsync(Guid sectionId)
    {
        var result = _mapper.Map<IEnumerable<TopicViewDto>>(
                await _topicRepository.GetTopicsBySectionAsync(sectionId)
                .ConfigureAwait(false))
            .ToArray();

        return result;
    }

    public override async Task<Guid> AddAsync(TopicPostDto obj)
    {
        var topic = _mapper.Map<Topic>(obj);

        var id = await _topicRepository.AddAsync(topic)
            .ConfigureAwait(false);

        var analyzedTopic = await ConvertTopicToElasticAsync(topic)
            .ConfigureAwait(false);
        await _elasticRepository.IndexDocumentAsync(analyzedTopic)
            .ConfigureAwait(false);

        return id;
    }

    public override async Task RemoveAsync(Guid id)
    { 
        await _topicRepository.RemoveAsync(id).ConfigureAwait(false);
        await _elasticRepository.DeleteDocumentAsync(id).ConfigureAwait(false);
    }

    public override async Task UpdateAsync(TopicUpdateDto obj)
    {
        Topic topic = _mapper.Map<Topic>(obj);
        await _topicRepository.UpdateAsync(topic)
            .ConfigureAwait(false);

        var analyzedTopic = await ConvertTopicToElasticAsync(topic)
            .ConfigureAwait(false);
        await _elasticRepository.UpdateDocumentAsync(analyzedTopic)
            .ConfigureAwait(false);
    }

    #region elastic

    public async Task<IEnumerable<TopicViewDto>> SearchAsync(string key)
        => _mapper.Map<IEnumerable<TopicViewDto>>(
            (await _elasticRepository
                    .SearchAsync(
                        await AnalyzeAsync(key)
                            .ConfigureAwait(false)
                    ).ConfigureAwait(false))
            .Documents.ToList());

    public async Task<IEnumerable<TopicViewDto>> SearchAsync()
        => _mapper.Map<IEnumerable<TopicViewDto>>(
           (await _elasticRepository
                   .SearchAllAsync()
                   .ConfigureAwait(false))
           .Documents.ToList());

    public async Task IndexDocumentAsync(TopicPostDto topic)
        => await _elasticRepository
            .IndexDocumentAsync(
                _mapper.Map<TopicElasticDto>(
                    _mapper.Map<Topic>(topic)
                )
            ).ConfigureAwait(false);

    public async Task UpdateDocumentAsync(TopicUpdateDto topic)
        => await _elasticRepository
            .UpdateDocumentAsync(
                _mapper.Map<TopicElasticDto>(
                    _mapper.Map<Topic>(topic)
                )
            ).ConfigureAwait(false);

    public async Task DeleteDocumentAsync(Guid id)
        => await _elasticRepository
            .DeleteDocumentAsync(id)
            .ConfigureAwait(false);

    public async Task<string> AnalyzeAsync(string str)
    {
        var response = await _elasticRepository
            .AnalyzeDocumentAsync(str)
            .ConfigureAwait(false);
        var query = string.Join(" ", response.Tokens.Select(t => t.Token));

        return query;
    }

    private async Task<TopicElasticDto> AnalyzeTopicAsync(TopicElasticDto topic)
    {
        topic.Description = await AnalyzeAsync(topic.Description)
            .ConfigureAwait(false);
        topic.Title = await AnalyzeAsync(topic.Title)
            .ConfigureAwait(false);

        return topic;
    }

    private async Task<TopicElasticDto> ConvertTopicToElasticAsync(Topic topic)
    {
        var mappedTopic = _mapper.Map<TopicElasticDto>(topic);
        var analyzedTopic = await AnalyzeTopicAsync(mappedTopic)
            .ConfigureAwait(false);
        return analyzedTopic;
    }

    private async Task MigrateDataAsync()
    {
        var data = await _topicRepository.GetAsync()
            .ConfigureAwait(false);

        foreach(var topic in data)
        {
            var analyzedTopic = await ConvertTopicToElasticAsync(topic)
                .ConfigureAwait(false);

            await _elasticRepository.IndexDocumentAsync(analyzedTopic)
                .ConfigureAwait(false);
        }
    }

    #endregion
}