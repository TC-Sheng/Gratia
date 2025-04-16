using Gratia.Api.Models;
using Gratia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gratia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(IEventService eventService, ILogger<EventController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> HandleSlackEvent([FromBody] object request)
    {
        try
        {
            // Try to deserialize as UrlVerificationRequest first
            var urlVerificationRequest = JsonConvert.DeserializeObject<UrlVerificationRequest>(request.ToString() ?? string.Empty);
            if (urlVerificationRequest?.Type == "url_verification")
            {
                logger.LogInformation("Received URL verification request: {@UrlVerificationRequest}", urlVerificationRequest);
                var challenge = await eventService.VerifyUrlAsync(urlVerificationRequest.Token, urlVerificationRequest.Challenge);
                if (challenge == null)
                {
                    logger.LogError("Invalid verification token");
                    return Unauthorized();
                }
                return Ok(new { challenge });
            }

            // If not URL verification, try EventRequest
            var eventRequest = JsonConvert.DeserializeObject<EventRequest>(request.ToString() ?? string.Empty);
            if (eventRequest == null)
            {
                logger.LogError("Invalid request format");
                return BadRequest("Invalid request format");
            }

            logger.LogInformation("Received Slack event: {@EventRequest}", eventRequest);

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
                return Ok(new { status = "success", message = "Event received and stored", event_id = eventId });
            }

            logger.LogError("Unsupported event type: {EventType}", eventRequest.RequestData.Event.Type);
            return BadRequest("Unsupported event type");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing request");
            return BadRequest("Invalid request format");
        }
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