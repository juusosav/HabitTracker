# HabitTracker

[Live Demo](https://habittracker-gzcqecg9g7gzcgee.canadacentral-01.azurewebsites.net/)

HabitTracker is a **Blazor Server** web application for tracking daily habits. Users can create, edit, complete, and delete habits. The app keeps track of completed habits, provides daily reset functionality, and includes statistics for habit completion over time.

ATTENTION: The current version of the app is only a demo, user-specific authentication and databases will be implemented later. Reset of habits per day is also offline, but can be found in the code.
---

## Features

- Create, edit, and delete habits
- Mark habits as completed
- Automatic daily reset of completed habits
- Track statistics (weekly, monthly)
- Persistent storage using SQLite
- Interactive and real-time UI using Blazor Server

---

## Technologies

- **Blazor Server** (.NET 8)
- **SQLite** database
- **Entity Framework Core** for database access
- **Blazored.LocalStorage** for local storage (optional)
- **Azure App Service** for deployment

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or 2023 (with ASP.NET and web development workload)
- SQLite (optional for local testing)

### Setup

1. Clone the repository:

```bash
git clone https://github.com/juusosav/HabitTracker.git
cd HabitTracker
```
2. Restore NuGet packages:

```bash
git clone https://github.com/juusosav/HabitTracker.git
cd HabitTracker
```

3. Run migrations to create the database:

```bash
dotnet ef database update
```

4. Run the app locally:

```bash
dotnet run
```

5. Open your browser at https://localhost:5001 or the URL shown in the console.
