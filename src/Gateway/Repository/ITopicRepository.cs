using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface ITopicRepository : IRepositoryCrud<Topic>
{
    public Task<IEnumerable<Topic>> GetTopicsBySectionAsync(Guid id);
}