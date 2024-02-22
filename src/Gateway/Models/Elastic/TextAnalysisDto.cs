using System.Text.Json.Serialization;

namespace Gateway.Models.Elastic;

public class TextAnalysisDto
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}