namespace Gratia.Api.Models;

public class SlackSettings
{
    public string ApiToken { get; set; } = string.Empty;
    public string AppLevelToken { get; set; } = string.Empty;
    public string SigningSecret { get; set; } = string.Empty;
    public string VerificationToken { get; set; } = string.Empty;
    public string[] BotResponses { get; set; } = Array.Empty<string>();
} 