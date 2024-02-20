using Gateway.Core.Models;

namespace Gateway.Models.Entities;

public partial class Topic : IIdentifiedObject
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<SectionTopic> SectionTopics { get; set; } = new List<SectionTopic>();
}