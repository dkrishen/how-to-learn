using System.Text.Json.Serialization;

namespace Gateway.Core.Models;

public class Queries
{
    [JsonPropertyName("pattern")]
    public string? Pattern { get; set; }

    [JsonPropertyName("page")]
    public int? Page { get; set; }

    [JsonPropertyName("size")]
    public int? PageSize { get; set; }
}
