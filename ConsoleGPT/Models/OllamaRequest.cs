namespace ConsoleGPT.Models;

public record OllamaRequest(List<Message> Messages)
{
    public string Model { get; init; } = "tinyllama";
    public bool Stream { get; init; } = false;
}