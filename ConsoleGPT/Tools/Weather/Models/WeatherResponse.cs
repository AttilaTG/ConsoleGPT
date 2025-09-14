using System.Text.Json.Serialization;

namespace ConsoleGPT.Tools.Weather.Models;

public record WeatherResponse
{
    [JsonPropertyName("location")]
    public required LocationModel Location { get; init; }
    
    [JsonPropertyName("current")]
    public required CurrentWeatherModel Current { get; init; }
}