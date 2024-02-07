using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;
using Gateway.Repository;

namespace Gateway.Logic;

public class SectionLogic : LogicCrud<Section, SectionViewDto, SectionPostDto, SectionUpdateDto>, ISectionLogic
{
    private readonly ISectionTopicRepository _sectionTopicRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly ITopicRepository _topicRepository;
    private readonly IMapper _mapper;

    public SectionLogic(ISectionRepository sectionRepository, ISectionTopicRepository sectionTopicRepository, ITopicRepository topicRepository, IMapper mapper)
        : base(sectionRepository, mapper)
    {
        _sectionTopicRepository = sectionTopicRepository;
        _sectionRepository = sectionRepository;
        _topicRepository = topicRepository;
        _mapper = mapper;
    }

    public override async Task<IEnumerable<SectionViewDto>> GetAsync()
        => _mapper.Map<IEnumerable<SectionViewDto>>(
            await _sectionRepository.GetFullSectionsAsync().ConfigureAwait(false));

    public override async Task<Guid> AddAsync(SectionPostDto section)
    { 
        var sectionId = await _sectionRepository.AddAsync(
            _mapper.Map<Section>(section)).ConfigureAwait(false);

        var topics = await _topicRepository.GetTopicsByTitlesAsync(section.Topics)
            .ConfigureAwait(false);

        foreach ( var topic in topics)
        {
            await _sectionTopicRepository.AddAsync(new SectionTopic
            {
                TopicId = topic.Id,
                SectionId = sectionId,
                Id = Guid.NewGuid()
            });
        }

        return sectionId;
    }
}