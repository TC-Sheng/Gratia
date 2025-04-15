using Gratia.Api.Models;

namespace Gratia.Api.Repositories;

public interface IEventRepository
{
    Task<Event?> GetByIdAsync(long id);
    Task<IEnumerable<Event>> GetAllAsync();
    Task<long> CreateAsync(Event @event);
    Task<bool> UpdateAsync(Event @event);
    Task<bool> DeleteAsync(long id);
} 