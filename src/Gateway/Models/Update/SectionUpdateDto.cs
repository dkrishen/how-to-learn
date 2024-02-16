using System.Text.Json.Serialization;

namespace Gateway.Models.Update;

public class SectionUpdateDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("topics")]
    public Guid[]? Topics { get; set; }
}
