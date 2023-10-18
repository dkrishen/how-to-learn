namespace Gateway.Models.Update;

public class TopicUpdateDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }
}
