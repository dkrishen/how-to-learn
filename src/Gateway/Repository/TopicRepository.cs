using Gateway.Data;
using Gateway.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gateway.Repository;

public class TopicRepository : RepositoryBase, ITopicRepository
{
    public TopicRepository(HowToLearnDbContext context) : base(context) { }

    public async Task<Topic> GetTopicAsync(Guid id)
        => await this.GetOperationAsync<Topic>(id).ConfigureAwait(false);

    public async Task<IEnumerable<Topic>> GetTopicsAsync()
        => await this.GetOperationAsync<Topic>().ConfigureAwait(false);

    public async Task<Guid> AddTopicAsync(Topic topic)
        => await this.AddOperationAsync(topic).ConfigureAwait(false);

    public async Task RemoveTopicAsync(Guid id)
        => await this.RemoveOperationAsync<Topic>(id).ConfigureAwait(false);

    public async Task UpdateTopicAsync(Topic updatedTopic)
        => await this.UpdateOperationAsync(updatedTopic).ConfigureAwait(false);

    public async Task<IEnumerable<string>> GetTopicNamesAsync()
        => await context.Topics
            .Select(t => t.Title)
            .ToListAsync().ConfigureAwait(false);

    public async Task<IEnumerable<Guid>> GetTopicIdsByTitlesAsync(string[] titles)
        => await context.Topics
            .Where(t => titles.Contains(t.Title))
            .Select(t => t.Id)
            .ToListAsync().ConfigureAwait(false);

    public async Task<IEnumerable<Topic>> GetTopicsByTitlesAsync(string[] titles)
        => await context.Topics
            .Where(t => titles.Contains(t.Title))
            .ToListAsync().ConfigureAwait(false);  
}