using Gratia.Api.Models;
using Gratia.Api.Repositories;
using Microsoft.Extensions.Options;
namespace Gratia.Api.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly string _verificationToken;

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

    public async Task<string?> VerifyUrlAsync(string token, string challenge)
    {
        if (token != _verificationToken)
        {
            return null;
        }

        return challenge;
    }
} 