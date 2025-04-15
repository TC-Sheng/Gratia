using Gratia.Api.Models;
using Gratia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SlackNet.AspNetCore;

namespace Gratia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(IEventService eventService, ILogger<EventController> logger, IOptions<SlackSettings> slackSettings) : ControllerBase
{
    private readonly string _verificationToken = slackSettings.Value.VerificationToken;

    [HttpPost]
    public async Task<IActionResult> HandleSlackEvent([FromBody] EventRequest eventRequest)
    {
        logger.LogInformation("Received Slack event: {@EventRequest}", eventRequest);

        // Handle URL verification
        if (eventRequest.RequestData.Type == "url_verification")
        {
            if (eventRequest.RequestData.Token != _verificationToken)
            {
                logger.LogError("Invalid verification token");
                return Unauthorized();
            }

            logger.LogInformation("URL verification successful");
            return Ok(new { challenge = eventRequest.RequestData.Event.Text });
        }

        if (eventRequest.RequestData.Event.Type == "app_mention")
        {
            var slackEvent = new SlackEvent
            {
                Type = eventRequest.RequestData.Event.Type,
                User = eventRequest.RequestData.Event.User,
                Channel = eventRequest.RequestData.Event.Channel,
                Text = eventRequest.RequestData.Event.Text
            };

            var eventId = await eventService.CreateEventAsync(slackEvent);
            logger.LogInformation("Created event with ID: {EventId}", eventId);
            return Ok(new { id = eventId });
        }

        logger.LogError("Unsupported event type: {EventType}", eventRequest.RequestData.Event.Type);
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