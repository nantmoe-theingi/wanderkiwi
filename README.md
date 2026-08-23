# WanderKiwi

WanderKiwi is a modern travel planning web application that helps users discover places to visit in New Zealand using AI-generated recommendations, interactive maps, weather forecasts, and personalized itineraries.

## Tech Stack

- .NET 8 Web API
- Angular
- SQL Server
- Azure
- Docker
- GitHub Actions

## Documentation

Detailed project documentation is available in the `/docs` folder.

## 🗄️ Database Seeding & Mock Data

This project utilizes Entity Framework Core code-first migrations to seed initial tourism data into the database. 

* **Scope:** The database includes **45 verified points of interest** across major New Zealand regions (Queenstown, Christchurch, and Auckland), featuring realistic road-accessible driving coordinates (latitude/longitude) for mapping and routing accuracy.
* **Data Purpose:** While geographic coordinates and attraction details are structured accurately, metrics such as **ratings and review counts are simulated seed values** intended to support UI development, card layouts, and filtering logic. 
* **Extensibility:** The data layer is decoupled via EF Core entities (`Attraction`, `Destination`, `Category`), making it ready for integration with live third-party tourism APIs (such as Google Places or TripAdvisor) in future iterations.
