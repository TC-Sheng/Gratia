﻿namespace SlackNet.Events;

/// <summary>
/// Sent when the topic for a channel is changed.
/// </summary>
public class ChannelTopic : MessageEvent
{
    public string Topic { get; set; }
}