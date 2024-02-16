using AutoMapper;
using Gateway.Core.Models;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;
using Gateway.Repository;
using Microsoft.Extensions.Options;

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

    public override async Task<Guid> AddAsync(TopicPostDto obj)
    {
        var topic = _mapper.Map<Topic>(obj);
        try
        {
            var id = await _topicRepository.AddAsync(topic)
                .ConfigureAwait(false);

        topic.Id = id;
        //await _elasticRepository.InsertDocumentAsync("",topic)
        //    .ConfigureAwait(false);

        }catch (Exception ex)
        {

        }
        return Guid.Empty;// id;

    }

    public async Task<TopicViewDto[]> GetTopicsBySectionAsync(Guid sectionId)
    {
        var result = _mapper.Map<IEnumerable<TopicViewDto>>(
                await _topicRepository.GetTopicsBySectionAsync(sectionId)
                .ConfigureAwait(false))
            .ToArray();

        return result;
    }

    public override async Task RemoveAsync(Guid id)
    { 
        await _topicRepository.RemoveAsync(id).ConfigureAwait(false);
        //await _elasticRepository.RemoveAsync(id).ConfigureAwait (false);
    }


    public override async Task UpdateAsync(TopicUpdateDto obj)
    {
        Topic topic = _mapper.Map<Topic>(obj);
        await _topicRepository.UpdateAsync(topic)
            .ConfigureAwait(false);

    //    await _elasticRepository.UpdateAsync(topic)
    //        .ConfigureAwait(false);
    }
}
