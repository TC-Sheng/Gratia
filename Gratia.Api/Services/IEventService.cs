using Gratia.Api.Models;

namespace Gratia.Api.Services;

public interface IEventService
{
    Task<SlackEvent?> GetEventByIdAsync(long id);
    Task<IEnumerable<SlackEvent>> GetAllEventsAsync();
    Task<long> CreateEventAsync(SlackEvent slackEvent);
    Task<bool> UpdateEventAsync(SlackEvent slackEvent);
    Task<bool> DeleteEventAsync(long id);
} 