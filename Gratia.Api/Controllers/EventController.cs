using Gratia.Api.Models;
using Gratia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using SlackNet.AspNetCore;
using SlackNet.Events;

namespace Gratia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost]
    public async Task<IActionResult> HandleSlackEvent([FromBody] EventCallback eventCallback)
    {
        if (eventCallback.Event is MessageEvent mentionEvent)
        {
            var slackEvent = new SlackEvent
            {
                Type = mentionEvent.Type,
                User = mentionEvent.User,
                Channel = mentionEvent.Channel,
                Text = mentionEvent.Text ?? string.Empty
            };

            var eventId = await _eventService.CreateEventAsync(slackEvent);
            return Ok(new { id = eventId });
        }

        return BadRequest("Unsupported event type");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SlackEvent>>> GetAllEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }
} 