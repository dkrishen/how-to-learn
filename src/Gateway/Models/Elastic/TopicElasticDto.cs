using Nest;

namespace Gateway.Models.Elastic;

public class TopicElasticDto
{
    public Guid Id { get; set; }

    [Text(Analyzer = "russian_analyzer")]
    public string Title { get; set; } = null!;

    [Text(Analyzer = "russian_analyzer")]
    public string? Description { get; set; }
}
