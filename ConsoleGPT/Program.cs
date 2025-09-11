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

Console.WriteLine("🤖 Welcome to ConsoleGPT!\n💬 Type your message to chat with AI\n🚪 Enter 'exit' or a whitespace to quit\n");

while (true)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("💬 : ");
    var userInput = Console.ReadLine();
    Console.WriteLine();
    Console.ResetColor();
    
    if (string.IsNullOrWhiteSpace(userInput) || userInput.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
    {
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
        
        Console.WriteLine($"🤖: {reply.Message.Content}\n");
        
        messages.Add(reply.Message);
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"❌ Connection Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Unexpected error: {ex.Message}");
    }
}