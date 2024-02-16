using System.Text.Json.Serialization;

namespace Gateway.Models.Delete;

public class SectionDeleteDto 
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}
