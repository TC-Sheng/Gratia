using Gratia.Api.Repositories;
using Gratia.Api.Services;
using SlackNet.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

// Get Slack configuration
var slackApiToken = builder.Configuration["Slack:ApiToken"] ?? throw new InvalidOperationException("Slack:ApiToken is not configured");
var slackAppToken = builder.Configuration["Slack:AppToken"] ?? throw new InvalidOperationException("Slack:AppToken is not configured");
var slackSigningSecret = builder.Configuration["Slack:SigningSecret"] ?? throw new InvalidOperationException("Slack:SigningSecret is not configured");

// Configure Slack
builder.Services.AddSlackNet(c => c
    .UseApiToken(slackApiToken)
    .UseAppLevelToken(slackAppToken)
    .UseSigningSecret(slackSigningSecret));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
