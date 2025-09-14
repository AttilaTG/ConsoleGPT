namespace ConsoleGPT.Services.Chat;

public interface IChatService
{
    Task<string> SendMessageAsync(string userMessage);
    void ClearHistory();
}