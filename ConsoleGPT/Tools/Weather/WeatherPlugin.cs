using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;
using ConsoleGPT.Configuration.Options;
using ConsoleGPT.Tools.Weather.Models;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;

namespace ConsoleGPT.Tools.Weather;

public class WeatherPlugin(IHttpClientFactory httpClientFactory, IOptions<WeatherPluginOptions>  options)
{
    private readonly JsonSerializerOptions _jsonOptions = new ()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
    };
    
    private readonly WeatherPluginOptions _options = options.Value;
    
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    [KernelFunction]
    [Description("Gets the current weather for a given city.")]
    public async Task<string> GetWeatherAsync(string city)
    {
        try
        {
            var url = string.Format(_options.CurrentWeatherEndpoint, city, _options.ApiKey);

            var response = await _httpClient.GetFromJsonAsync<WeatherResponse>(url, _jsonOptions);

            return JsonSerializer.Serialize(response) 
                   ?? throw new InvalidOperationException($"Could not get current weather for {city}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not get current weather for {city}", ex);
        }
    }
}