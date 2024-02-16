using AutoMapper;
using Gateway.Core.Models;
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
    private readonly IElasticRepository _elasticRepository;
    private readonly ITopicRepository _topicRepository;
    private readonly IMapper _mapper;

    public SectionLogic(ISectionRepository sectionRepository, ISectionTopicRepository sectionTopicRepository, ITopicRepository topicRepository, IMapper mapper, IElasticRepository elasticRepository)
        : base(sectionRepository, mapper)
    {
        _sectionTopicRepository = sectionTopicRepository;
        _sectionRepository = sectionRepository;
        _elasticRepository = elasticRepository;
        _topicRepository = topicRepository;
        _mapper = mapper;
    }

    public override async Task<DataWithSlicePagination<SectionViewDto>> GetAsync(Queries? options)
    {
        DataWithSlicePagination<SectionViewDto> result = new DataWithSlicePagination<SectionViewDto>()
        {
            Items = null,
            IsLast = true
        };

        if (options == null)
        {
            result.Items = _mapper.Map<IEnumerable<SectionViewDto>>(
                await _sectionRepository.GetFullSectionsAsync()
                    .ConfigureAwait(false))
                .ToArray();
        }
        else
        {
            result.Items = _mapper.Map<IEnumerable<SectionViewDto>>(
                await _sectionRepository.GetFullSectionsAsync((int)options.Page, (int)options.PageSize)
                    .ConfigureAwait(false))
                .ToArray();

            result.IsLast = (int)options.Page * (int)options.PageSize
                    >= (await _sectionRepository.GetCountAsync()
                        .ConfigureAwait(false))
                ? true : false;
        }

        return result;
    }
        //=> _mapper.Map<IEnumerable<SectionViewDto>>(
        //    await _sectionRepository.GetFullSectionsAsync().ConfigureAwait(false));

    public override async Task<Guid> AddAsync(SectionPostDto section)
    { 
        var sectionId = await _sectionRepository.AddAsync(
            _mapper.Map<Section>(section)).ConfigureAwait(false);

        foreach (var id in section.Topics)
        {
            await _sectionTopicRepository.AddAsync(new SectionTopic
            {
                TopicId = id,
                SectionId = sectionId,
                Id = Guid.NewGuid()
            });
        }

        return sectionId;
    }

    public override async Task UpdateAsync(SectionUpdateDto section)
    {
        await _sectionRepository.UpdateAsync(
            _mapper.Map<Section>(section)).ConfigureAwait(false);

        var oldTopics = await _topicRepository.GetTopicsBySectionAsync(section.Id)
            .ConfigureAwait(false);

        var topicsForRemove = oldTopics.Select(t => t.Id)
            .Distinct()
            .Except(section.Topics.ToList())
            .ToArray();
        var topicsForAdd = section.Topics.ToList()
            .Distinct()
            .Except(oldTopics.Select(t => t.Id))
            .ToArray();

        foreach(var topicId in topicsForRemove)
        {
            await _sectionTopicRepository.RemoveAsync(section.Id, topicId)
                .ConfigureAwait(false);
        }
        foreach(var topicId in topicsForAdd)
        {
            await _sectionTopicRepository.AddAsync(new SectionTopic()
                {
                    Id = Guid.NewGuid(),
                    SectionId = section.Id,
                    TopicId = topicId
                })
                .ConfigureAwait(false);
        }
    }

    public async Task<IEnumerable<SectionViewDto>> GenerateResponse(string request)
    {
        //var topics = _elasticRepository.GetAsync(request)
        //    .ConfigureAwait(false);

        throw new NotImplementedException("");

        //return await _sectionRepository.GetFullSectionsByTopicsAsync(new IEnumerable<Guid>())
        //    .ConfigureAwait(false);
    }
}