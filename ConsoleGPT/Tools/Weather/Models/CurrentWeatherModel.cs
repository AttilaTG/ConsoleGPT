using System.Text.Json.Serialization;

namespace ConsoleGPT.Tools.Weather.Models;

public record CurrentWeatherModel
{
     [JsonPropertyName("temp_c")]
     public required double TempC { get; init; }
     
     [JsonPropertyName("wind_kph")]
     public required double WindKph { get; init; }
     
     [JsonPropertyName("wind_dir")]
     public required string WindDir { get; init; }
     
     [JsonPropertyName("precip_mm")]
     public required double PrecipMm { get; init; }
     
     [JsonPropertyName("humidity")]
     public required int Humidity { get; init; }
     
     [JsonPropertyName("feelslike_c")]
     public required double FeelslikeC { get; init; }
     
     [JsonPropertyName("vis_km")]
     public required double VisKm { get; init; }
     
     [JsonPropertyName("uv")]
     public required double Uv { get; init; }
}