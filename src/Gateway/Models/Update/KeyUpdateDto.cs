namespace Gateway.Models.Update;

public class KeyUpdateDto
{
    public Guid Id { get; set; }

    public Guid Parent { get; set; }

    public Guid Reference { get; set; }
}
