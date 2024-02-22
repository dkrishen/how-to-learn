using Gateway.Models.Elastic;
using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface ISectionRepository : IRepositoryCrud<Section>
{
    public Task<IEnumerable<RowResponseDto>> GetSectionsByTopicsAsync(IEnumerable<Guid> topicIds);
}