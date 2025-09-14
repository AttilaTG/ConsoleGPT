using ConsoleGPT.Services.Chat;

namespace ConsoleGPT.Services.ConsoleApp;

public class ConsoleService(IChatService chatService) : IConsoleService
{
    public async Task RunAsync()
    {
        Console.WriteLine("🤖 Welcome to ConsoleGPT!\n💬 Type your message to chat with AI\n🚪 Enter 'exit' or a whitespace to quit\n");
        Console.WriteLine("💡 Commands: '/clear' to clear history, '/help' - to show the available commands\n");

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
            
            if (userInput.StartsWith("/"))
            {
                HandleCommand(userInput);
                continue;
            }
            
            try
            {
                var response = await chatService.SendMessageAsync(userInput);
                
                Console.WriteLine($"🤖: {response}\n");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Error: {ex.Message}\n");
                Console.ResetColor();
            }
        }
    }
    
    private void HandleCommand(string command)
    {
        switch (command.ToLower())
        {
            case "/clear":
                chatService.ClearHistory();
                Console.WriteLine("🗑️  Chat history cleared!\n");
                break;
            
            case "/help":
                Console.WriteLine("📋 Available commands:");
                Console.WriteLine("  /clear   - Clear chat history");
                Console.WriteLine("  /help    - Show this help");
                Console.WriteLine("  exit     - Quit the application\n");
                break;
                
            default:
                Console.WriteLine($"❌ Unknown command: {command}. Type '/help' for available commands.\n");
                break;
        }
    }
}