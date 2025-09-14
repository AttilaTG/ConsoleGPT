using ConsoleGPT.Configuration.Options;
using ConsoleGPT.Services.Chat;
using ConsoleGPT.Services.ConsoleApp;
using ConsoleGPT.Tools.Weather;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("settings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var ollamaHost = Environment.GetEnvironmentVariable("OLLAMA_HOST")  
                         ?? "http://localhost:11434";
        var uri = new Uri(ollamaHost);
        services.AddOllamaChatCompletion("llama3.2:1b", uri);

        services.AddOptions<WeatherPluginOptions>()
            .Bind(hostContext.Configuration.GetSection("Plugins:WeatherPlugin"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IConsoleService, ConsoleService>();
        
        services.AddScoped<WeatherPlugin>();
        
        services.AddHttpClient();
        
        services.AddTransient<Kernel>((serviceProvider) => {
            var kernel = new Kernel(serviceProvider);
            
            var weatherPlugin = serviceProvider.GetRequiredService<WeatherPlugin>();
            kernel.Plugins.AddFromObject(weatherPlugin, "weather");
            
            return kernel;
        });
        
    }).Build();

try
{
    var consoleService = host.Services.GetRequiredService<IConsoleService>();
    await consoleService.RunAsync();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"💥 Fatal error: {ex.Message}");
    Console.ResetColor();
}
    