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

    public async Task<IEnumerable<SlackEvent>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<SlackEvent>("SELECT * FROM SlackEvents");
    }

    public async Task<long> CreateAsync(SlackEvent slackEvent)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"
            INSERT INTO SlackEvents (Type, [User], Channel, Text, CreatedAt, UpdatedAt)
            VALUES (@Type, @User, @Channel, @Text, GETDATE(), GETDATE());
            SELECT CAST(SCOPE_IDENTITY() as INT)";
        
        return await connection.QueryFirstOrDefaultAsync<long>(sql, slackEvent);
    }
} 