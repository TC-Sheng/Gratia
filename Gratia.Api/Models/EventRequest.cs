using System.Text.Json.Serialization;

namespace Gratia.Api.Models;

public class EventRequest
{
    [JsonPropertyName("request_data")]
    public RequestData RequestData { get; set; } = new();
    
    [JsonPropertyName("ip")]
    public string Ip { get; set; } = string.Empty;
}

public class RequestData
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
    
    [JsonPropertyName("team_id")]
    public string TeamId { get; set; } = string.Empty;
    
    [JsonPropertyName("api_app_id")]
    public string ApiAppId { get; set; } = string.Empty;
    
    [JsonPropertyName("event")]
    public SlackEventData Event { get; set; } = new();
    
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("event_id")]
    public string EventId { get; set; } = string.Empty;
    
    [JsonPropertyName("event_time")]
    public long EventTime { get; set; }
    
    [JsonPropertyName("authorizations")]
    public List<Authorization> Authorizations { get; set; } = new();
    
    [JsonPropertyName("is_ext_shared_channel")]
    public bool IsExtSharedChannel { get; set; }
    
    [JsonPropertyName("event_context")]
    public string EventContext { get; set; } = string.Empty;
}

public class SlackEventData
{
    [JsonPropertyName("user")]
    public string User { get; set; } = string.Empty;
    
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("ts")]
    public string Ts { get; set; } = string.Empty;
    
    [JsonPropertyName("client_msg_id")]
    public string ClientMsgId { get; set; } = string.Empty;
    
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
    
    [JsonPropertyName("team")]
    public string Team { get; set; } = string.Empty;
    
    [JsonPropertyName("blocks")]
    public List<Block> Blocks { get; set; } = new();
    
    [JsonPropertyName("channel")]
    public string Channel { get; set; } = string.Empty;
    
    [JsonPropertyName("event_ts")]
    public string EventTs { get; set; } = string.Empty;
}

public class Block
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("block_id")]
    public string BlockId { get; set; } = string.Empty;
    
    [JsonPropertyName("elements")]
    public List<Element> Elements { get; set; } = new();
}

public class Element
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("elements")]
    public List<Element> Elements { get; set; } = new();
    
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;
    
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("unicode")]
    public string Unicode { get; set; } = string.Empty;
}

public class Authorization
{
    [JsonPropertyName("enterprise_id")]
    public string? EnterpriseId { get; set; }
    
    [JsonPropertyName("team_id")]
    public string TeamId { get; set; } = string.Empty;
    
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;
    
    [JsonPropertyName("is_bot")]
    public bool IsBot { get; set; }
    
    [JsonPropertyName("is_enterprise_install")]
    public bool IsEnterpriseInstall { get; set; }
} 