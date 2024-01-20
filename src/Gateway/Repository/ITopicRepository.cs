﻿using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface ITopicRepository
{
    public Task<Topic> GetTopicAsync(Guid id);
    public Task<IEnumerable<Topic>> GetTopicsAsync();
    public Task<Guid> AddTopicAsync(Topic section);
    public Task RemoveTopicAsync(Guid id);
    public Task UpdateTopicAsync(Topic section);

    public Task<IEnumerable<string>> GetTopicNamesAsync();
    public Task<IEnumerable<Guid>> GetTopicIdsByTitlesAsync(string[] titles);
    public Task<IEnumerable<Topic>> GetTopicsByTitlesAsync(string[] titles);
}
