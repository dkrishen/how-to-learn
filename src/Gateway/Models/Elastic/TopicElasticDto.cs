namespace Gateway.Models.Elastic;

public class TopicElasticDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }
}
