using Gratia.Api.Models;
using Gratia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SlackNet.AspNetCore;
using SlackNet.Events;

namespace Gratia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(IEventService eventService, ILogger<EventController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> HandleSlackEvent([FromBody] EventCallback eventCallback)
    {
        logger.LogInformation("Received Slack event: {@EventCallback}", eventCallback);
        
        if (eventCallback.Event is MessageEvent mentionEvent)
        {
            var slackEvent = new SlackEvent
            {
                Type = mentionEvent.Type,
                User = mentionEvent.User,
                Channel = mentionEvent.Channel,
                Text = mentionEvent.Text ?? string.Empty
            };

            var eventId = await eventService.CreateEventAsync(slackEvent);
            logger.LogInformation("Created event with ID: {EventId}", eventId);
            return Ok(new { id = eventId });
        }

        logger.LogError("Unsupported event type: {EventType}", eventCallback.Event?.Type);
        return BadRequest("Unsupported event type");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SlackEvent>>> GetAllEvents()
    {
        logger.LogInformation("Getting all events");
        var events = await eventService.GetAllEventsAsync();
        logger.LogInformation("Retrieved {Count} events", events.Count());
        return Ok(events);
    }
} 