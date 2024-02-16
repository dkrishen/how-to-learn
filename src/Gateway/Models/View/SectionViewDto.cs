using System.Text.Json.Serialization;

namespace Gateway.Models.View;

public class SectionViewDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("topics")]
    public string[] Topics { get; set; } = null!;
}
