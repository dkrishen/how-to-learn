using Gateway.Data;
using Gateway.Models.Entities;

namespace Gateway.Repository
{
    public class SectionRepository : RepositoryBase, ISectionRepository
    {
        public SectionRepository(HowToLearnDbContext context) : base(context) { }

        public async Task<Section> GetSectionAsync(Guid id)
            => await this.GetOperationAsync<Section>(id).ConfigureAwait(false);

        public async Task<IEnumerable<Section>> GetSectionsAsync()
            => await this.GetOperationAsync<Section>().ConfigureAwait(false);

        public async Task AddSectionAsync(Section section)
            => await this.AddOperationAsync(section).ConfigureAwait(false);

        public async Task RemoveSectionAsync(Guid id)
            => await RemoveOperationAsync<Section>(id).ConfigureAwait(false);

        public async Task UpdateSectionAsync(Section updatedSection)
            => await this.UpdateOperationAsync(updatedSection).ConfigureAwait(false);
    }
}