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

    [HttpGet("{id}")]
    public async Task<ActionResult<SlackEvent>> GetEvent(long id)
    {
        var @event = await _eventService.GetEventByIdAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        return @event;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SlackEvent>>> GetAllEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(long id, SlackEvent @event)
    {
        if (id != @event.Id)
        {
            return BadRequest();
        }

        var success = await _eventService.UpdateEventAsync(@event);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(long id)
    {
        var success = await _eventService.DeleteEventAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
} 