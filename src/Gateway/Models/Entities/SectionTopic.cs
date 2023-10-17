using Gateway.Core.Models;

namespace Gateway.Models.Entities;

public partial class SectionTopic : IIdentifiedObject
{
    public Guid SectionId { get; set; }

    public Guid TopicId { get; set; }

    public Guid Id { get; set; }

    public virtual Section Section { get; set; } = null!;

    public virtual Topic Topic { get; set; } = null!;
}