using Gateway.Data;
using Gateway.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gateway.Repository;

public class SectionTopicRepository : RepositoryBase, ISectionTopicRepository
{
    public SectionTopicRepository(HowToLearnDbContext context) : base(context) { }

    public async Task RemoveAsync(Guid sectionId, Guid topicId)
    {
        var obj = await context.SectionTopics
            .SingleAsync(st => st.SectionId == sectionId && st.TopicId == topicId)
            .ConfigureAwait(false);

        context.SectionTopics.Remove(obj);

        await context
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }

    public async Task RemoveBySectionAsync(Guid sectionId)
    {
        var objs = await context.SectionTopics
            .Where(st => st.SectionId == sectionId)
            .ToListAsync()
            .ConfigureAwait(false);

        context.SectionTopics.RemoveRange(objs);

        await context
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }

    public async Task<Guid> AddAsync(SectionTopic sectionTopic)
    {
        await context.SectionTopics
            .AddAsync(sectionTopic)
            .ConfigureAwait(false);

        await context
            .SaveChangesAsync()
            .ConfigureAwait(false);

        return sectionTopic.Id;
    }
}