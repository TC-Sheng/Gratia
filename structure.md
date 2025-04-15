# ASP.NET Core Structure

## Project Overview
This project demonstrates how to integrate SlackNet with an ASP.NET Core application, showcasing basic event handling and message interaction.

## Project Dependencies
This project is tightly coupled with SlackNet and cannot function without it. Key dependencies include:

1. **Core Dependencies**
   - SlackNet.AspNetCore package
   - SlackNet event handling system
   - Slack API client implementation

2. **Critical Dependencies**
   - `AddSlackNet()` service registration
   - `UseSlackNet()` middleware
   - Slack event handlers
   - API client interfaces

3. **Functionality Dependencies**
   - Slack event processing
   - API communication
   - Request validation
   - Endpoint handling

Without SlackNet, the project would lose all Slack-related functionality and cannot operate as intended.

## Project Structure

### 1. Files
- **Program.cs**: Application entry point and configuration
- **appsettings.json**: Configuration settings

### 2. Configuration
```json
{
  "Slack": {
    "ApiToken": "xoxb-...",
    "AppLevelToken": "xapp-...",
    "SigningSecret": "..."
  }
}
```

## Key Components

### 1. SlackNet Integration
- **Service Registration**: Configured in `Program.cs`
  - API Token authentication
  - App Level Token (optional, for socket mode)
  - Signing Secret for request validation
  - Event handler registration

### 2. Endpoint Configuration
Default Slack endpoints:
- `/slack/event`: Event subscriptions
- `/slack/action`: Interactive components
- `/slack/options`: Select menus
- `/slack/command`: Slash commands

### 3. Implementation
- **Purpose**: Demonstrates basic message event handling
- **Functionality**:
  - Listens for messages containing "ping"
  - Logs user and channel information
  - Responds with "pong" message
- **Dependencies**:
  - `ISlackApiClient`: For Slack API interactions
  - `ILogger`: For logging

## Features
1. Basic Slack event handling
2. Message response functionality
3. User and channel information retrieval
4. Logging integration
5. Configuration management
6. Socket mode support (configurable)

## Development Setup
1. Configure Slack settings in appsettings.json
2. Register event handlers in Program.cs
3. Configure endpoint routing
4. Enable/disable socket mode as needed 