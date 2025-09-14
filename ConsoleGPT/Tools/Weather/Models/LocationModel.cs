using System.Text.Json.Serialization;

namespace ConsoleGPT.Tools.Weather.Models;

public record LocationModel
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("region")]
    public required string Region { get; init; }
    
    [JsonPropertyName("country")]
    public required string Country { get; init; }
    
    [JsonPropertyName("localtime")]
    public required string Localtime { get; init; }
}