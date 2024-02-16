using System.Text.Json.Serialization;

namespace Gateway.Models.Post;

public class TopicPostDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}