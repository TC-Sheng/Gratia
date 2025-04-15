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
  - Models (for input/output/request/response classes)
- Uses Dapper for database operations
- Configuration:
  - Database connection string in appsettings.json
  - Uses QueryFirstOrDefaultAsync for database queries

### Gratia.Db
- SQL Server Database Project
- Contains:
  - Table scripts
  - Data scripts
  - Stored procedure scripts
  - Database publishing configurations

## Features

### Slack Integration
- Receives and processes Slack API requests
- Stores request data in SQL Server
- Sends messages to Slack API

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
