using Gateway.Models.Elastic;
using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface ISectionRepository : IRepositoryCrud<Section>
{
    public Task<IEnumerable<Section>> GetFullSectionsAsync();
    public Task<IEnumerable<Section>> GetFullSectionsAsync(int page, int pageSize);
    public Task<IEnumerable<RowResponseDto>> GetFullSectionsByTopicsAsync(IEnumerable<Guid> topicIds);
}