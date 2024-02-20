using Gateway.Core.Models;
using Gateway.Data;
using Gateway.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gateway.Repository;

public class TopicRepository : RepositoryCrud<Topic>, ITopicRepository
{
    public TopicRepository(HowToLearnDbContext context) : base(context) { }

    public async Task<IEnumerable<string>> GetTopicNamesAsync()
        => await context.Topics
            .Select(t => t.Title)
            .ToListAsync().ConfigureAwait(false);

    public async Task<IEnumerable<Guid>> GetTopicIdsByTitlesAsync(string[] titles)
        => await context.Topics
            .Where(t => titles.Contains(t.Title))
            .Select(t => t.Id)
            .ToListAsync().ConfigureAwait(false);

    public async Task<Guid> GetTopicIdByTitleAsync(string title)
        => (await context.Topics
            .FirstOrDefaultAsync(t => t.Title == title)
            .ConfigureAwait(false)).Id;

    public async Task<IEnumerable<Topic>> GetTopicsByTitlesAsync(string[] titles)
        => await context.Topics
            .Where(t => titles.Contains(t.Title))
            .ToListAsync().ConfigureAwait(false);

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