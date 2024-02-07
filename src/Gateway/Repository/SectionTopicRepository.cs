using Gateway.Data;
using Gateway.Models.Entities;

namespace Gateway.Repository;

public class SectionTopicRepository : RepositoryCrud<SectionTopic>, ISectionTopicRepository
{
    public SectionTopicRepository(HowToLearnDbContext context) : base(context) { }
}