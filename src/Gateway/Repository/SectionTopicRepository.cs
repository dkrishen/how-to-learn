using Gateway.Data;
using Gateway.Models.Entities;

namespace Gateway.Repository;

public class SectionTopicRepository : RepositoryBase, ISectionTopicRepository
{
    public SectionTopicRepository(HowToLearnDbContext context) : base(context) { }

    public async Task<SectionTopic> GetSectionTopicAsync(Guid id)
        => await this.GetOperationAsync<SectionTopic>(id).ConfigureAwait(false);

    public async Task<IEnumerable<SectionTopic>> GetSectionTopicsAsync()
        => await this.GetOperationAsync<SectionTopic>().ConfigureAwait(false);

    public async Task<Guid> AddSectionTopicAsync(SectionTopic sectionTopic)
        => await this.AddOperationAsync(sectionTopic).ConfigureAwait(false);

    public async Task RemoveSectionTopicAsync(Guid id)
        => await this.RemoveOperationAsync<SectionTopic>(id).ConfigureAwait(false);

    public async Task UpdateSectionTopicAsync(SectionTopic updatedSectionTopic)
        => await this.UpdateOperationAsync(updatedSectionTopic).ConfigureAwait(false);
}