using AutoMapper;
using Gateway.Core.Models;
using Gateway.Models.Elastic;
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
    private readonly double _validRanking;

    public SectionLogic(ISectionRepository sectionRepository, ISectionTopicRepository sectionTopicRepository, ITopicRepository topicRepository, IMapper mapper, IElasticRepository elasticRepository)
        : base(sectionRepository, mapper)
    {
        _sectionTopicRepository = sectionTopicRepository;
        _sectionRepository = sectionRepository;
        _elasticRepository = elasticRepository;
        _topicRepository = topicRepository;
        _mapper = mapper;
        _validRanking = 0.45;
    }

    public override async Task<DataWithSlicePaginationDto<SectionViewDto>> GetAsync(QueriesRequestDto? options)
    {
        DataWithSlicePaginationDto<SectionViewDto> result = new DataWithSlicePaginationDto<SectionViewDto>()
        {
            Items = null,
            IsLast = true
        };

        if (options == null)
        {
            result.Items = _mapper.Map<IEnumerable<SectionViewDto>>(
                await _sectionRepository.GetAsync()
                    .ConfigureAwait(false))
                .ToArray();
        }
        else
        {
            result.Items = _mapper.Map<IEnumerable<SectionViewDto>>(
                await _sectionRepository.GetAsync((int)options.Page, (int)options.PageSize)
                    .ConfigureAwait(false))
                .ToArray();

            result.IsLast = (int)options.Page * (int)options.PageSize
                    >= (await _sectionRepository.GetCountAsync()
                        .ConfigureAwait(false))
                ? true : false;
        }

        return result;
    }

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

    public async Task<IEnumerable<SectionResponseDto>> GenerateResponseAsync(string request)
    {
        var analyzedRequest = await AnalyzeAsync(request)
            .ConfigureAwait(false);

        var topics = (await _elasticRepository
            .SearchAsync(analyzedRequest)
            .ConfigureAwait(false));

        var filteredTopicIds = topics.Hits
            .Where(h => h.Score >= topics.MaxScore * _validRanking)
            .Select(h => h.Source.Id)
            .ToList();

        var rows = await _sectionRepository
            .GetSectionsByTopicsAsync(filteredTopicIds)
            .ConfigureAwait(false);

        return MapRowsToView(rows);
    }

    private async Task<string> AnalyzeAsync(string str)
    {
        var response = await _elasticRepository
            .AnalyzeDocumentAsync(str)
            .ConfigureAwait(false);
        var query = string.Join(" ", response.Tokens.Select(t => t.Token));

        return query;
    }

    private IEnumerable<SectionResponseDto> MapRowsToView(IEnumerable<RowResponseDto> rows)
        => rows.GroupBy(r => r.SectionId)
            .Select(g => new SectionResponseDto()
            {
                Id = g.Key,
                Title = g.FirstOrDefault().SectionTitle,
                Topics = g.Select(r => new TopicViewDto() {
                   Title = r.TopicTitle,
                   Id = r.TopicId,
                   Description = r.TopicDescription
                } ).ToArray()
            })
            .ToList();
}