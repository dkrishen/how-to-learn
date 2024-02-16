using System.Text.Json.Serialization;
using Gateway.Models.View;

namespace Gateway.Core.Models;

public class DataWithSlicePagination<T> : Data<T> where T : class
{
    [JsonPropertyName("last")]
    public bool IsLast { get; set; }
}
