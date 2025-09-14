using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace ConsoleGPT.Services.Chat;

public class ChatService : IChatService
{
    private readonly IChatCompletionService _chatService; 
    private ChatHistory _chatHistory = new ChatHistory();
    private readonly string _systemPrompt;

    public ChatService(IChatCompletionService chatCompletionService)
    {
        _chatService = chatCompletionService;
        _systemPrompt = "You are TinyLlama, a helpful assistant for chatting. Be concise and friendly.";
        InitializeChatHistory();
    }
    
    public async Task<string> SendMessageAsync(string userMessage)
    {
        _chatHistory.AddUserMessage(userMessage);
        try
        {
            var response = await _chatService.GetChatMessageContentsAsync(
                _chatHistory,new PromptExecutionSettings
                {
                    ExtensionData = new Dictionary<string, object>
                    {
                        ["temperature"] = 0.7,
                        ["max_tokens"] = 1024
                    }
                });

            var assistantResponse = response[0].Content;

            if (assistantResponse is null)
            {
                throw new InvalidOperationException("Couldn't get response from request");
            }

            _chatHistory.AddAssistantMessage(assistantResponse);
            
            return assistantResponse;
        }
        catch (Exception ex)
        {
            throw new Exception($"❌ Unexpected error: {ex.Message}");
        }
    }

    public void ClearHistory()
    {
        InitializeChatHistory();
    }

    private void InitializeChatHistory()
    {
        _chatHistory = new ChatHistory();
        _chatHistory.AddSystemMessage(_systemPrompt);
    }
}