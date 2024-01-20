using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface ISectionTopicRepository
{
    public Task<SectionTopic> GetSectionTopicAsync(Guid id);
    public Task<IEnumerable<SectionTopic>> GetSectionTopicsAsync();
    public Task<Guid> AddSectionTopicAsync(SectionTopic sectionTopic);
    public Task RemoveSectionTopicAsync(Guid id);
    public Task UpdateSectionTopicAsync(SectionTopic updatedSectionTopic);
}
