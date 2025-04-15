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

    public async Task<Event?> GetEventByIdAsync(long id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return await _eventRepository.GetAllAsync();
    }

    public async Task<long> CreateEventAsync(Event @event)
    {
        return await _eventRepository.CreateAsync(@event);
    }

    public async Task<bool> UpdateEventAsync(Event @event)
    {
        return await _eventRepository.UpdateAsync(@event);
    }

    public async Task<bool> DeleteEventAsync(long id)
    {
        return await _eventRepository.DeleteAsync(id);
    }
} 