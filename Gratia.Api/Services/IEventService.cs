using Gratia.Api.Models;

namespace Gratia.Api.Services;

public interface IEventService
{
    Task<SlackEvent?> GetEventByIdAsync(long id);
    Task<IEnumerable<SlackEvent>> GetAllEventsAsync();
    Task<long> CreateEventAsync(SlackEvent @event);
    Task<bool> UpdateEventAsync(SlackEvent @event);
    Task<bool> DeleteEventAsync(long id);
} 