using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface ISectionRepository
{
    public Task<Section> GetSectionAsync(Guid id);
    public Task<IEnumerable<Section>> GetSectionsAsync();
    public Task<Guid> AddSectionAsync(Section section);
    public Task RemoveSectionAsync(Guid id);
    public Task UpdateSectionAsync(Section section);

    public Task<IEnumerable<Section>> GetFullSectionsAsync();
}
