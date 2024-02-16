using Gateway.Models.Entities;

namespace Gateway.Repository;

public interface ISectionTopicRepository
{
    public Task RemoveAsync(Guid sectionId, Guid topicId);
    public Task RemoveBySectionAsync(Guid sectionId);
    public Task<Guid> AddAsync(SectionTopic sectionTopic);
}
