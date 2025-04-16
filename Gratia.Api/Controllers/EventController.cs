using Gratia.Api.Models;
using Gratia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SlackNet;
using SlackNet.WebApi;

namespace Gratia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(IEventService eventService, ILogger<EventController> logger, ISlackApiClient slack) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> HandleSlackEvent([FromBody] object request)
    {
        try
        {
            // Try to deserialize as UrlVerificationRequest first
            var requestJson = request.ToString() ?? string.Empty;
            var urlVerificationRequest = JsonSerializer.Deserialize<UrlVerificationRequest>(requestJson);
            if (urlVerificationRequest?.Type == "url_verification")
            {
                logger.LogInformation("Received URL verification: {@UrlVerificationRequest}", urlVerificationRequest);
                var challenge = eventService.VerifyUrl(urlVerificationRequest.Token, urlVerificationRequest.Challenge);
                if (challenge == null)
                {
                    logger.LogError("Invalid verification token");
                    return Unauthorized();
                }
                
                var urlVerificationResponse = new { challenge };
                logger.LogInformation("URL verification response: {@UrlVerificationResponse}", urlVerificationResponse);
                return Ok(urlVerificationResponse);
            }

            // If not URL verification, try EventRequest
            var eventRequest = new EventRequest
            {
                RequestData = JsonSerializer.Deserialize<RequestData>(requestJson) ?? new RequestData()
            };

            if (eventRequest.RequestData == null)
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

                logger.LogInformation("Posting message to channel: {Channel}", slackEvent.Channel);
                var messageText = "Bot Test";
                await slack.Chat.PostMessage(new Message
                {
                    Text = messageText,
                    Channel = slackEvent.Channel
                });
                logger.LogInformation("Message posted to channel: {Channel}， message: {Message}", slackEvent.Channel, messageText);

                var slackEventResponse = new { status = "success", message = "Event received and stored", event_id = eventId };
                logger.LogInformation("Slack event response: {@SlackEventResponse}", slackEventResponse);
                return Ok(slackEventResponse);
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