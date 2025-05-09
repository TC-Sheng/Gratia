# Gratia

## Project Structure

Gratia consists of two main subprojects:

### Gratia.Api
- ASP.NET Core 8.0 Web API project
- Handles Slack API integration
- Architecture:
  - Controllers
  - Services
  - Repositories
  - Models
- Uses Dapper for database operations
- Configuration:
  - Database connection string in appsettings.json
  - Logging configuration in appsettings.json

### Gratia.Db
- SQL Server Database Project
- Contains:
  - Table scripts

## Current Features

### Slack Integration
1. Event Handling
   - URL verification for Slack events
   - App mention detection
   - Message content validation with regex
   - Bot response generation

2. Bot Responses
   - Random appreciation messages
   - Configurable responses in appsettings.json
   - Emoji support in responses

3. Message Processing
   - Stores event data in database
   - Sends bot responses to channels

## Setup Instructions

1. Prerequisites:
   - .NET 8.0 SDK
   - SQL Server (LocalDB or full instance)
   - Visual Studio 2022 or VS Code

2. Database Setup:
   - Run the scripts in Gratia.Db project

3. API Configuration:
   - Update the database connection string in `appsettings.json`
   - Set your Slack API tokens in `appsettings.json`:
     ```json
     "Slack": {
       "ApiToken": "your-bot-user-oauth-token",
       "AppLevelToken": "your-app-level-token",
       "SigningSecret": "your-signing-secret",
       "VerificationToken": "your-verification-token"
     }
     ```

4. API Endpoints:
   - POST /api/event - Handle Slack events
   - GET /api/event - Get all events

## Slack Integration

1. Create a Slack App:
   - Go to https://api.slack.com/apps
   - Create a new app
   - Enable Event Subscriptions, 
   - Subscribe to bot events: `app_mention`, `message.channels`
   - Add Scopes in OAuth & Permissions
    - Bot Token Scopes:
      `app_mentions:read`,`channels:history`,`channels:read`,`chat:write`,`emoji:read`,`groups:history`,`groups:read`,`im:read`,`im:write`,`users:read`
    - User Token Scopes:
      `chat:write`
   - Install the app to your workspace

2. Configure Slack Tokens:
   - Copy Bot User OAuth Token to `ApiToken`
   - Copy App-Level OAuth Token to `AppLevelToken` (Optional, required for socket mode)
   - Copy Signing Secret to `SigningSecret`
   - Copy Verification Token to `VerificationToken`

3. Set Event URL:
   - Set the Request URL to your API endpoint: `https://your-domain/api/event`

## Database Structure

### SlackEvents table
- id: Primary key
- type: Event type
- user: User ID
- channel: Channel ID
- text: Message content
- created_at: Creation timestamp
- updated_at: Update timestamp

## Change App Name / Bot Name
- Change App name 
  - Go to https://api.slack.com/apps
  - Settings > Basic Information > Display Information
- Change App Display Name 
  - Go to https://api.slack.com/apps
  - Features > App Home > App Display Name
- Change Bot tag
  - Go to Slack Application
  - Click Your App Icon > About > Configuration > Bot User