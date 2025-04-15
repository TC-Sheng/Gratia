namespace Gratia.Api.Models;

public class Event
{
    public long Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
} 