namespace Gateway.Models.View;

public class TopicViewDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }
}
