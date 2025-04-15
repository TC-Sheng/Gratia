namespace Gratia.Api.Models;

public class SlackSettings
{
    public string ApiToken { get; init; } = string.Empty;
    public string AppLevelToken { get; init; } = string.Empty;
    public string SigningSecret { get; init; } = string.Empty;
} 