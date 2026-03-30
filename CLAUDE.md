# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Gampix** is a sports betting API built with ASP.NET Core 8.0. It manages users, games, and betting transactions with a layered architecture.

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Language**: C#
- **API Documentation**: Swagger/OpenAPI (via Swashbuckle.AspNetCore)
- **Build System**: .NET CLI

## Architecture & Project Structure

The API follows a **three-layer architecture**:

```
gampix.api/
├── Domain/                 # Core business entities
│   ├── User.cs            # User entity with balance and bets collection
│   ├── Game.cs            # Game/Event entity with odds and status
│   └── Bet.cs             # Bet entity (includes BetStatus enum)
├── Services/              # Business logic layer with dependency injection
│   ├── IUserService.cs    # User service interface + DTOs
│   ├── UserService.cs     # User service implementation
│   ├── IGameService.cs    # Game service interface + DTOs
│   ├── GameService.cs     # Game service implementation
│   ├── IBetService.cs     # Bet service interface + DTOs
│   └── BetService.cs      # Bet service implementation
├── Controllers/           # HTTP endpoint layer
│   ├── UsersController.cs # User CRUD endpoints
│   ├── GamesController.cs # Game CRUD endpoints
│   └── BetsController.cs  # Bet endpoints + status updates
├── Program.cs             # Service registration and app configuration
└── appsettings.json       # Configuration

```

## Core Entities

### User
- **Id**: Unique identifier
- **Username**: User's display name
- **Email**: User's email address
- **Balance**: Account balance for betting
- **CreatedAt**: Registration timestamp
- **Bets**: Collection of user's bets

### Game
- **Id**: Unique identifier
- **Name**: Game/event name (e.g., "Barcelona vs Real Madrid")
- **Description**: Event details
- **StartDate**: When the game starts
- **EndDate**: When the game ends (deadline for betting)
- **OddsWin**: Odds multiplier for winning bets
- **IsActive**: Whether bets can be placed on this game
- **Bets**: Collection of bets placed on this game

### Bet
- **Id**: Unique identifier
- **UserId**: Foreign key to User
- **GameId**: Foreign key to Game
- **Amount**: Bet amount in currency units
- **Status**: BetStatus enum (Pending, Won, Lost, Cancelled)
- **Result**: Outcome description (set after game ends)
- **Winnings**: Calculated payout (null for pending/lost bets)
- **CreatedAt**: Timestamp of bet placement

## Common Development Commands

### Build
```bash
dotnet build
```

### Run
```bash
dotnet run
```
The API runs on `http://localhost:5000` by default (check `appsettings.json` for port configuration).

### Test Endpoints
Use the included `gampix.api.http` file with REST Client extensions (VS Code REST Client or Rider) to test endpoints.

## API Endpoints

### Users
- `GET /api/users` - List all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### Games
- `GET /api/games` - List all games
- `GET /api/games/{id}` - Get game by ID
- `POST /api/games` - Create game
- `PUT /api/games/{id}` - Update game
- `DELETE /api/games/{id}` - Delete game

### Bets
- `GET /api/bets` - List all bets
- `GET /api/bets/{id}` - Get bet by ID
- `GET /api/bets/user/{userId}` - Get user's bets
- `GET /api/bets/game/{gameId}` - Get bets on a game
- `POST /api/bets` - Create bet
- `PUT /api/bets/{id}/status` - Update bet status (won/lost)

## Data Storage

Services currently use in-memory collections (static Lists). When ready to persist data:
1. Replace static Lists with Entity Framework Core DbContext
2. Update service implementations to use `_dbContext`
3. Consider adding migrations for schema management

## Error Handling

All controllers follow consistent error handling:
- `BadRequest` (400) for validation errors
- `NotFound` (404) for missing resources
- `StatusCode(500)` for unexpected exceptions
- Errors are logged via `ILogger<T>`

## Important Notes

- **Port Configuration**: Currently defaults to port 5000. Check `appsettings.Development.json` for development overrides.
- **Nullable Reference Types**: Enabled via `<Nullable>enable</Nullable>` in the project file.
- **DTOs**: CreateUserRequest, UpdateUserRequest, CreateGameRequest, UpdateGameRequest, CreateBetRequest are defined alongside their service interfaces.
- **In-Memory Storage**: IDs are auto-incremented with static counters. This will need refactoring for persistence.
