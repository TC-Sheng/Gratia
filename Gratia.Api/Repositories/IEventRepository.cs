using Gratia.Api.Models;

namespace Gratia.Api.Repositories;

public interface IEventRepository
{
    Task<SlackEvent?> GetByIdAsync(long id);
    Task<IEnumerable<SlackEvent>> GetAllAsync();
    Task<long> CreateAsync(SlackEvent @event);
    Task<bool> UpdateAsync(SlackEvent @event);
    Task<bool> DeleteAsync(long id);
} 