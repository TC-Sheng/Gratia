using Gratia.Api.Models;
using Gratia.Api.Repositories;

namespace Gratia.Api.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<SlackEvent?> GetEventByIdAsync(long id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<SlackEvent>> GetAllEventsAsync()
    {
        return await _eventRepository.GetAllAsync();
    }

    public async Task<long> CreateEventAsync(SlackEvent slackEvent)
    {
        return await _eventRepository.CreateAsync(slackEvent);
    }

    public async Task<bool> UpdateEventAsync(SlackEvent slackEvent)
    {
        return await _eventRepository.UpdateAsync(slackEvent);
    }

    public async Task<bool> DeleteEventAsync(long id)
    {
        return await _eventRepository.DeleteAsync(id);
    }
} 