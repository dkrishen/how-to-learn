using Gateway.Core.Models;

namespace Gateway.Models.Entities;

public partial class Key : IIdentifiedObject
{
    public Guid Id { get; set; }

    public Guid Parent { get; set; }

    public Guid Reference { get; set; }

    public virtual Topic ParentNavigation { get; set; } = null!;

    public virtual Topic ReferenceNavigation { get; set; } = null!;
}
