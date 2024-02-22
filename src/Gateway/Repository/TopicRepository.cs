using Gateway.Core.Models;
using Gateway.Data;
using Gateway.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gateway.Repository;

public class TopicRepository : RepositoryCrud<Topic>, ITopicRepository
{
    public TopicRepository(HowToLearnDbContext context) : base(context) { }

    public async Task<IEnumerable<Topic>> GetTopicsBySectionAsync(Guid id)
        => await context.Topics
            .Include(t => t.SectionTopics)
            .ThenInclude( st => st.Section)
            .Where(t => t.SectionTopics.Select(st => st.SectionId).Contains(id))
            .ToListAsync().ConfigureAwait(false);

    protected override async Task<IEnumerable<T>> GetOperationAsync<T>(string pattern) where T : class
        => (IEnumerable<T>)(await context.Topics
            .Where(o => o.Title.Contains(pattern))
            .ToListAsync()
            .ConfigureAwait(false));
}