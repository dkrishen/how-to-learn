using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;
using Gateway.Repository;

namespace Gateway.Logic;

public class TopicLogic : LogicCrud<Topic, TopicViewDto, TopicPostDto, TopicUpdateDto>, ITopicLogic
{
    private readonly ITopicRepository _topicRepository;
    private readonly IMapper _mapper;

    public TopicLogic(ITopicRepository topicRepository, IMapper mapper)
        : base(topicRepository, mapper)
    {
        _topicRepository = topicRepository;
        _mapper = mapper;
    }
}
