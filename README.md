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
  - Uses QueryFirstOrDefaultAsync for database queries
  - Logging configuration in appsettings.json:
    ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        },
        "File": {
          "Path": "logs/gratia-{Date}.log",
          "MinimumLevel": "Information"
        }
      }
    }
    ```

### Gratia.Db
- SQL Server Database Project
- Contains:
  - Table scripts
  - Data scripts
  - Stored procedure scripts

## Setup Instructions

1. Prerequisites:
   - .NET 8.0 SDK
   - SQL Server (LocalDB or full instance)
   - Visual Studio 2022 or VS Code

2. Database Setup:
   - Open the solution in Visual Studio
   - Right-click on the Gratia.Db project
   - Select "Publish"
   - Configure your database connection
   - Click "Publish"

3. API Configuration:
   - Update the connection string in `Gratia.Api/appsettings.json`
   - Set your Slack API tokens in `appsettings.json`:
     ```json
     "Slack": {
       "ApiToken": "your-bot-user-oauth-token",
       "AppToken": "your-app-level-token",
       "SigningSecret": "your-signing-secret"
     }
     ```

4. Running the API:
   ```bash
   cd Gratia.Api
   dotnet run
   ```

5. API Endpoints:
   - POST /api/event - Handle Slack events
   - GET /api/event - Get all events
   - GET /api/event/{id} - Get event by ID
   - PUT /api/event/{id} - Update event
   - DELETE /api/event/{id} - Delete event

## Slack Integration

1. Create a Slack App:
   - Go to https://api.slack.com/apps
   - Create a new app
   - Enable Event Subscriptions
   - Subscribe to bot events: `app_mention`
   - Install the app to your workspace

2. Configure Slack Tokens:
   - Copy Bot User OAuth Token to `ApiToken`
   - Copy App-Level Token to `AppToken`
   - Copy Signing Secret to `SigningSecret` (found in Basic Information > App Credentials)

3. Set Event URL:
   - Set the Request URL to your API endpoint: `https://your-domain/api/event`

## Features

### Slack Integration
- Receives and processes Slack API requests
- Stores request data in SQL Server
- Sends messages to Slack API
- Verifies request authenticity using signing secret

### Database Operations
- Uses Dapper for efficient data access
- Centralized database scripts management
- Supports database publishing and versioning

## API Specifications

### 1. Event API
```
POST /api/event
Purpose: Handle Slack events
Request Body:
{
    "token": "verification_token",
    "team_id": "team_id",
    "event": {
        "type": "app_mention",
        "user": "user_id",
        "text": "message_content",
        "channel": "channel_id"
    },
    "type": "event_callback"
}
Response:
- Success: 200 OK
- Failure: Appropriate error status code
```

## Database Structure

### events table
- id: Primary key
- type: Event type
- user: User ID
- channel: Channel ID
- text: Message content
- created_at: Creation timestamp
- updated_at: Update timestamp
