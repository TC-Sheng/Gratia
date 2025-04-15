using Gratia.Api.Models;

namespace Gratia.Api.Repositories;

public interface IEventRepository
{
    Task<SlackEvent?> GetByIdAsync(long id);
    Task<IEnumerable<SlackEvent>> GetAllAsync();
    Task<long> CreateAsync(SlackEvent slackEvent);
    Task<bool> UpdateAsync(SlackEvent slackEvent);
    Task<bool> DeleteAsync(long id);
} 