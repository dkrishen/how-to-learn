using Gateway.Data;
using Gateway.Models.Elastic;
using Gateway.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Repository;

public class SectionRepository : RepositoryCrud<Section>, ISectionRepository
{
    public SectionRepository(HowToLearnDbContext context) : base(context) { }

    public async Task<IEnumerable<Section>> GetFullSectionsAsync()
        => await context.Sections
            .Include(s => s.SectionTopics)
            .ThenInclude(st => st.Topic)
            .ToListAsync().ConfigureAwait(false);

    public async Task<IEnumerable<Section>> GetFullSectionsAsync(int page, int pageSize)
        => await context.Sections
            .Skip(page * pageSize)
            .Take(pageSize)
            .Include(s => s.SectionTopics)
            .ThenInclude(st => st.Topic)
            .ToListAsync().ConfigureAwait(false);

    public async Task<IEnumerable<RowResponseDto>> GetFullSectionsByTopicsAsync(IEnumerable<Guid> topicIds)
        => await context.Sections
            .Join(context.SectionTopics, s => s.Id, st => st.SectionId, (s, st) => new { Section = s, SectionTopic = st })
            .Join(context.Topics, st => st.SectionTopic.TopicId, t => t.Id, (st, t) => new { SectionTopic = st, Topic = t })
            .Where(stt => topicIds.Contains(stt.Topic.Id))
            .Select(stt => new RowResponseDto()
            {
                SectionId = stt.SectionTopic.Section.Id,
                SectionTitle = stt.SectionTopic.Section.Title,
                TopicTitle = stt.Topic.Title
            })
            .ToListAsync();
}