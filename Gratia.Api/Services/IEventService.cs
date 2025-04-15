using Gratia.Api.Models;

namespace Gratia.Api.Services;

public interface IEventService
{
    Task<Event?> GetEventByIdAsync(long id);
    Task<IEnumerable<Event>> GetAllEventsAsync();
    Task<long> CreateEventAsync(Event @event);
    Task<bool> UpdateEventAsync(Event @event);
    Task<bool> DeleteEventAsync(long id);
} 