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

    public async Task<SlackEvent?> GetByIdAsync(long id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<SlackEvent>(
            "SELECT * FROM Events WHERE Id = @Id",
            new { Id = id });
    }

    public async Task<IEnumerable<SlackEvent>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<SlackEvent>("SELECT * FROM Events");
    }

    public async Task<long> CreateAsync(SlackEvent slackEvent)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"
            INSERT INTO Events (Type, [User], Channel, Text, CreatedAt, UpdatedAt)
            VALUES (@Type, @User, @Channel, @Text, GETUTCDATE(), GETUTCDATE());
            SELECT CAST(SCOPE_IDENTITY() as INT)";
        
        return await connection.QueryFirstOrDefaultAsync<long>(sql, slackEvent);
    }

    public async Task<bool> UpdateAsync(SlackEvent slackEvent)
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
        
        var rowsAffected = await connection.ExecuteAsync(sql, slackEvent);
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