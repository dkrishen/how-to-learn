using System.Text.Json.Serialization;

namespace Gateway.Models.Post;

public class SectionPostDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;
    
    [JsonPropertyName("topics")]
    public Guid[]? Topics { get; set; }
}