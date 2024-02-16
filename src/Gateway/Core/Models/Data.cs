using System.Text.Json.Serialization;

namespace Gateway.Core.Models;

public class Data<T> where T : class
{
    [JsonPropertyName("items")]
    public T[] Items { get; set; }
}
