using Gateway.Models.PostDto;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic
{
    public interface ISectionLogic
    {
        public Task<SectionViewDto> GetSectionAsync(Guid id);
        public Task<IEnumerable<SectionViewDto>> GetSectionsAsync();
        public Task AddSectionAsync(SectionPostDto section);
        public Task UpdateSectionAsync(SectionUpdateDto section);
        public Task DeleteSectionAsync(Guid id);
    }
}