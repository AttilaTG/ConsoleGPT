using System.Text.Json.Serialization;

namespace ConsoleGPT.Models;

public record Message(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content
);