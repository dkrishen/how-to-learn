using Gateway.Data;
using Gateway.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Repository;

public class SectionRepository : RepositoryCrud<Section>, ISectionRepository
{
    public SectionRepository(HowToLearnDbContext context) : base(context) { }

    public async Task<IEnumerable<Section>> GetFullSectionsAsync()
        => await context.Sections
            .Include(s => s.SectionTopics)
            .ThenInclude(st => st.Topic)
            .ToListAsync().ConfigureAwait(false);
}