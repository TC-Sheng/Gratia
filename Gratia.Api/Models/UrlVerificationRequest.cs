using System.Text.Json.Serialization;

namespace Gratia.Api.Models;

public class UrlVerificationRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
    
    [JsonPropertyName("challenge")]
    public string Challenge { get; set; } = string.Empty;
} 