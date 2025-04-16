using Gratia.Api.Models;
using Gratia.Api.Repositories;
using Microsoft.Extensions.Options;
namespace Gratia.Api.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly string _verificationToken;
    private readonly Random _random = new();
    private readonly string[] _botResponses;

    public EventService(IEventRepository eventRepository, IOptions<SlackSettings> slackSettings)
    {
        _eventRepository = eventRepository;
        _verificationToken = slackSettings.Value.VerificationToken;
        _botResponses = slackSettings.Value.BotResponses;
    }

    public async Task<long> CreateEventAsync(SlackEvent slackEvent)
    {
        return await _eventRepository.CreateAsync(slackEvent);
    }

    public async Task<IEnumerable<SlackEvent>> GetAllEventsAsync()
    {
        return await _eventRepository.GetAllAsync();
    }

    public string? VerifyUrl(string token, string challenge)
    {
        if (token != _verificationToken)
        {
            return null;
        }

        return challenge;
    }

    public string GenerateBotMessage()
    {
        var index = _random.Next(_botResponses.Length);
        return _botResponses[index];
    }
} 