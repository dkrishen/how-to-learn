using Gateway.Core.Models;

namespace Gateway.Models.Entities;

public partial class Key : IIdentifiedObject
{
    public Guid Id { get; set; }

    public string Value { get; set; }

    public Guid Reference { get; set; }

    public virtual Topic ReferenceNavigation { get; set; } = null!;
}
