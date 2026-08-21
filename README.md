# Survey Application

A full-stack survey form application built with **ASP.NET Core MVC** and **Entity Framework Core**, allowing users to submit responses that are stored in a SQL Server database. A copy of each submitted response is automatically emailed to the survey host as a PDF.

## Features

- 📝 Dynamic survey form with fields for personal details, satisfaction ratings, and open-ended feedback
- 💾 Persists responses to a SQL Server database using EF Core Code-First migrations
- 📧 Automatically emails a PDF copy of each response to the survey host via Gmail SMTP (MailKit)
- 📄 Generates a PDF summary of each submitted survey
- 🗄️ Uses LocalDB for local development, easily swappable for full SQL Server in production

## Tech Stack

- **Backend:** ASP.NET Core MVC (.NET 8/9)
- **ORM:** Entity Framework Core
- **Database:** SQL Server (LocalDB for dev)
- **Email:** MailKit / MimeKit (SMTP via Gmail)
- **PDF Generation:** *(add your PDF library here, e.g. QuestPDF / iText)*

## Project Structure

```
surveyform/
├── Controllers/       # MVC controllers (survey submission logic)
├── Data/               # DbContext and database configuration
├── Migrations/         # EF Core migrations
├── Models/             # Data models (SurveyResponse, etc.)
├── Services/            # EmailService and other business logic
├── Views/               # Razor views for the survey form and results
├── wwwroot/            # Static assets (CSS, JS, images)
├── appsettings.json    # App configuration (connection strings, email settings)
└── Program.cs          # App entry point and service configuration
```

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 or later)
- SQL Server or SQL Server LocalDB
- A Gmail account with an [App Password](https://myaccount.google.com/apppasswords) generated for SMTP access

### Setup

1. **Clone the repository**
   ```bash
   git clone <your-repo-url>
   cd surveyform
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure the app** — see [Configuration](#configuration) below

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

The app will be available at `https://localhost:{port}` (check your console output for the exact URL).

## Configuration

Add the following to `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SurveyAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "EmailSettings": {
    "SenderEmail": "your-gmail@gmail.com",
    "SenderPassword": "your-app-password",
    "ReceiverEmail": "survey-host-email@gmail.com"
  }
}
```

| Key | Description |
|---|---|
| `ConnectionStrings:DefaultConnection` | SQL Server / LocalDB connection string |
| `EmailSettings:SenderEmail` | Gmail account the app sends from (must have an App Password set up) |
| `EmailSettings:SenderPassword` | The 16-character Gmail App Password (not your regular password) |
| `EmailSettings:ReceiverEmail` | The survey host's email — where response copies are sent |

> ⚠️ **Never commit real credentials to source control.** Use [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) in development or environment variables in production:
> ```bash
> dotnet user-secrets init
> dotnet user-secrets set "EmailSettings:SenderPassword" "your-app-password"
> ```

## Database Migrations

If you make changes to the models, create and apply a new migration:

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## License

*(Add your license here, e.g. MIT)*
