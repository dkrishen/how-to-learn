using System.Text.Json.Serialization;

namespace Gateway.Models.Delete;

public class TopicDeleteDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}
