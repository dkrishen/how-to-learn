using System.Text.Json.Serialization;

namespace Gateway.Models.Post;

public class PostData
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
}
