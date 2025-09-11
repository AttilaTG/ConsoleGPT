using System.Text.Json.Serialization;

namespace ConsoleGPT.Models;

public record OllamaChatResponse(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("message")] Message Message,
    [property: JsonPropertyName("done")] bool Done
);