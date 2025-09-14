using System.ComponentModel.DataAnnotations;

namespace ConsoleGPT.Configuration.Options;

public record WeatherPluginOptions
{
    [Required]
    public required string CurrentWeatherEndpoint { get; init; }
    
    [Required]
    public required string ApiKey { get; init; }
}
