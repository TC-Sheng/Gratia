using Gratia.Api.Models;

namespace Gratia.Api.Services;

public interface IEventService
{
    Task<long> CreateEventAsync(SlackEvent slackEvent);
    Task<IEnumerable<SlackEvent>> GetAllEventsAsync();
} 