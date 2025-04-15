namespace Gratia.Api.Models;

public class EventRequest
{
    public RequestData RequestData { get; set; } = new();
    public string Ip { get; set; } = string.Empty;
}

public class RequestData
{
    public string Token { get; set; } = string.Empty;
    public string TeamId { get; set; } = string.Empty;
    public string ApiAppId { get; set; } = string.Empty;
    public SlackEventData Event { get; set; } = new();
    public string Type { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public long EventTime { get; set; }
    public List<Authorization> Authorizations { get; set; } = new();
    public bool IsExtSharedChannel { get; set; }
    public string EventContext { get; set; } = string.Empty;
}

public class SlackEventData
{
    public string User { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Ts { get; set; } = string.Empty;
    public string ClientMsgId { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Team { get; set; } = string.Empty;
    public List<Block> Blocks { get; set; } = new();
    public string Channel { get; set; } = string.Empty;
    public string EventTs { get; set; } = string.Empty;
}

public class Block
{
    public string Type { get; set; } = string.Empty;
    public string BlockId { get; set; } = string.Empty;
    public List<Element> Elements { get; set; } = new();
}

public class Element
{
    public string Type { get; set; } = string.Empty;
    public List<Element> Elements { get; set; } = new();
    public string UserId { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Unicode { get; set; } = string.Empty;
}

public class Authorization
{
    public string? EnterpriseId { get; set; }
    public string TeamId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool IsBot { get; set; }
    public bool IsEnterpriseInstall { get; set; }
} 