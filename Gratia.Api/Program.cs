using Gratia.Api.Models;
using Gratia.Api.Repositories;
using Gratia.Api.Services;
using Serilog;
using SlackNet.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

// Configure Slack
var slackSettings = builder.Configuration.GetSection("Slack").Get<SlackSettings>() 
    ?? throw new InvalidOperationException("Slack settings are not configured");

builder.Services.AddSlackNet(c => c
    .UseApiToken(slackSettings.ApiToken)
    .UseAppLevelToken(slackSettings.AppLevelToken)
    .UseSigningSecret(slackSettings.SigningSecret));

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

app.Run();
