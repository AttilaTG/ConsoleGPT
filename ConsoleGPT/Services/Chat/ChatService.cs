using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;

namespace ConsoleGPT.Services.Chat;

public class ChatService : IChatService
{
    private readonly IChatCompletionService _chatService; 
    private ChatHistory _chatHistory = new ChatHistory();
    private readonly Kernel _kernel;
    private readonly string _systemPrompt;
    private readonly OllamaPromptExecutionSettings  _executionSettings = new ()
    {
        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
        ExtensionData = new Dictionary<string, object>
        {
            ["temperature"] = 0.7,
            ["max_tokens"] = 1024
        }
    };
    
    public ChatService(IChatCompletionService chatCompletionService,  Kernel kernel)
    {
        _chatService = chatCompletionService;
        _systemPrompt = "You are Llama, a helpful assistant for chatting. Be concise and friendly.";
        _kernel = kernel;
        InitializeChatHistory();
    }
    
    public async Task<string> SendMessageAsync(string userMessage)
    {
        _chatHistory.AddUserMessage(userMessage);
        try
        {
            var response = await _chatService.GetChatMessageContentsAsync(
                _chatHistory,
                _executionSettings,
                _kernel);

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