using Dapper;
using Gratia.Api.Models;
using Microsoft.Data.SqlClient;

namespace Gratia.Api.Repositories;

public class EventRepository : IEventRepository
{
    private readonly string _connectionString;

    public EventRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<Event?> GetByIdAsync(long id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Event>(
            "SELECT * FROM Events WHERE Id = @Id",
            new { Id = id });
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Event>("SELECT * FROM Events");
    }

    public async Task<long> CreateAsync(Event @event)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"
            INSERT INTO Events (Type, [User], Channel, Text)
            VALUES (@Type, @User, @Channel, @Text);
            SELECT CAST(SCOPE_IDENTITY() as BIGINT)";
        
        return await connection.QueryFirstAsync<long>(sql, @event);
    }

    public async Task<bool> UpdateAsync(Event @event)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"
            UPDATE Events 
            SET Type = @Type,
                [User] = @User,
                Channel = @Channel,
                Text = @Text,
                UpdatedAt = GETUTCDATE()
            WHERE Id = @Id";
        
        var rowsAffected = await connection.ExecuteAsync(sql, @event);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(
            "DELETE FROM Events WHERE Id = @Id",
            new { Id = id });
        return rowsAffected > 0;
    }
} 