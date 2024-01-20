using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;
using Gateway.Repository;

namespace Gateway.Logic;

public class SectionLogic : ISectionLogic
{
    private readonly ISectionTopicRepository _sectionTopicRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly ITopicRepository _topicRepository;
    private readonly IMapper _mapper;

    public SectionLogic(ISectionRepository sectionRepository, ISectionTopicRepository sectionTopicRepository, ITopicRepository topicRepository, IMapper mapper)
    {
        _sectionTopicRepository = sectionTopicRepository;
        _sectionRepository = sectionRepository;
        _topicRepository = topicRepository;
        _mapper = mapper;
    }

    public async Task<SectionViewDto> GetSectionAsync(Guid id)
        => _mapper.Map<SectionViewDto>(
            await _sectionRepository.GetSectionAsync(id).ConfigureAwait(false));

    public async Task<IEnumerable<SectionViewDto>> GetSectionsAsync()
        => _mapper.Map<IEnumerable<SectionViewDto>>(
            await _sectionRepository.GetFullSectionsAsync().ConfigureAwait(false));

    public async Task AddSectionAsync(SectionPostDto section)
    { 
        var sectionId = await _sectionRepository.AddSectionAsync(
            _mapper.Map<Section>(section)).ConfigureAwait(false);

        var topics = await _topicRepository.GetTopicsByTitlesAsync(section.Topics)
            .ConfigureAwait(false);

        foreach ( var topic in topics)
        {
            await _sectionTopicRepository.AddSectionTopicAsync(new SectionTopic
            {
                TopicId = topic.Id,
                SectionId = sectionId,
                Id = Guid.NewGuid()
            });
        }
    }

    public async Task DeleteSectionAsync(Guid id)
        => await _sectionRepository.RemoveSectionAsync(id).ConfigureAwait(false);

    public async Task UpdateSectionAsync(SectionUpdateDto section)
        => await _sectionRepository.UpdateSectionAsync(
            _mapper.Map<Section>(section)).ConfigureAwait(false);
}