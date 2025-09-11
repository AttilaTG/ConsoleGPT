using System.Net.Http.Json;
using ConsoleGPT.Models;

var ollamaHost = Environment.GetEnvironmentVariable("OLLAMA_HOST") 
                 ?? throw new Exception("OLLAMA_HOST environment variable is not set");
var client = new HttpClient()
{
    BaseAddress = new Uri(ollamaHost)
};

var messages = new List<Message>
{
    new Message("system", "You are TinyLlama, a helpful assistant for chatting. Be concise and friendly.")
};

Console.WriteLine("🤖 Welcome to ConsoleGPT!\n💬 Type your message to chat with AI\n🚪 Enter 'exit' or a whitespace to quit");

while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("💬 : ");
    var userInput = Console.ReadLine();
    
    if (string.IsNullOrWhiteSpace(userInput) || userInput.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
    {
        Console.ResetColor();
        break;
    }
    
    messages.Add(new Message("user", userInput));
    
    var request = new OllamaRequest(messages);
    
    try
    {
        var response = await client.PostAsJsonAsync("/api/chat", request);
        
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"❌ HTTP Error: {response.StatusCode}");
            continue;
        }
        
        var reply = await response.Content.ReadFromJsonAsync<OllamaChatResponse>();

        if (reply is null) 
        {
            Console.WriteLine("❌ Reply was empty.");
            continue;
        }
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"🤖: {reply.Message.Content}\n");
        Console.ResetColor();
        
        messages.Add(reply.Message);
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"❌ Connection Error: {ex.Message}");
    }
    catch (TaskCanceledException)
    {
        Console.WriteLine("❌ Timeout: Request was cancelled");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Unexpected error: {ex.Message}");
    }
}