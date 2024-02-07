using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface ISectionRepository : IRepositoryCrud<Section>
{
    public Task<IEnumerable<Section>> GetFullSectionsAsync();
}