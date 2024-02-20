using Gateway.Models.View;
using System.Text.Json.Serialization;

namespace Gateway.Models.Elastic;

public class SectionResponseDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("topics")]
    public TopicViewDto[] Topics { get; set; }
}
