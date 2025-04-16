using Gratia.Api.Models;
using Gratia.Api.Repositories;
using Microsoft.Extensions.Options;
namespace Gratia.Api.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly string _verificationToken;
    private readonly Random _random = new();
    private readonly string[] _appreciationResponses = new[]
    {
        "🌟 That's so kind of you! I've recorded this appreciation moment.",
        "💖 Your words of appreciation are truly heartwarming!",
        "✨ What a beautiful way to show gratitude!",
        "🙏 Your appreciation has been noted and shared.",
        "💫 Spreading positivity one appreciation at a time!",
        "🎉 Another moment of gratitude captured!",
        "💝 Your kind words make the world a better place.",
        "🌈 Thank you for sharing this appreciation!",
        "🌺 Gratitude is the best attitude!",
        "💌 Your appreciation message has been saved!"
    };

    public EventService(IEventRepository eventRepository, IOptions<SlackSettings> slackSettings)
    {
        _eventRepository = eventRepository;
        _verificationToken = slackSettings.Value.VerificationToken;
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
        var index = _random.Next(_appreciationResponses.Length);
        return _appreciationResponses[index];
    }
} 