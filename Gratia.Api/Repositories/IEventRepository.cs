using Gratia.Api.Models;

namespace Gratia.Api.Repositories;

public interface IEventRepository
{
    Task<long> CreateAsync(SlackEvent slackEvent);
    Task<IEnumerable<SlackEvent>> GetAllAsync();
} 