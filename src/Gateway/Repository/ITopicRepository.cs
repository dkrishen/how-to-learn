using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface ITopicRepository : IRepositoryCrud<Topic>
{
    public Task<IEnumerable<string>> GetTopicNamesAsync();
    public Task<IEnumerable<Guid>> GetTopicIdsByTitlesAsync(string[] titles);
    public Task<Guid> GetTopicIdByTitleAsync(string title);
    public Task<IEnumerable<Topic>> GetTopicsBySectionAsync(Guid id);


    public Task<IEnumerable<Topic>> GetTopicsByTitlesAsync(string[] titles);
}