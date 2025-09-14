using ConsoleGPT.Services.Chat;
using ConsoleGPT.Services.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var ollamaHost = Environment.GetEnvironmentVariable("OLLAMA_HOST")  
                         ?? "http://localhost:11434";
        var uri = new Uri(ollamaHost);
        services.AddOllamaChatCompletion("tinyllama", uri);

        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IConsoleService, ConsoleService>();

        services.AddKernel();
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
    