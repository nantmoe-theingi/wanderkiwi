# WanderKiwi

WanderKiwi is a modern full-stack travel planning web application that helps users discover destinations in New Zealand using AI-generated recommendations, interactive maps, weather forecasts, and personalized itineraries.

---

## 🏛️ System Architecture

WanderKiwi follows a clean, decoupled architecture separating the Angular SPA frontend from the .NET 8 Web API backend, integrated with cloud hosting and external routing services.

### High-Level Architecture
![High-Level Architecture](docs/high%20level.png)

*(You can also reference your detailed [Clean Architecture Layer Diagram](docs/Clean%20Architecture.png) and [Deployment Architecture](docs/Deployment%20architecture.png) for a deeper breakdown of the infrastructure, CI/CD pipelines, and project layers).*

---

## 🚀 Tech Stack

- **Backend:** .NET 8 Web API, Entity Framework Core, LINQ, SQL Server
- **Frontend:** Angular, TypeScript
- **Routing & Navigation:** OpenRouteService (ORS) API
- **Cloud & DevOps:** Azure App Service, Azure SQL, Docker, GitHub Actions CI/CD

---

## 🗄️ Database Seeding & Mock Data

This project utilizes Entity Framework Core code-first migrations to seed initial tourism data into the database. 

* **Scope:** The database includes **50 verified points of interest** across major New Zealand regions (Queenstown, Christchurch, Auckland, and Te Anau). All locations feature precise, road-accessible driving coordinates (latitude/longitude) optimized for routing engines like **OpenRouteService (ORS)**.
* **Data Purpose:** While geographic coordinates and attraction details are structured accurately to support mapping and navigation logic, metrics such as **ratings and review counts are simulated seed values** intended for UI development, card layouts, and filtering tests. 
* **Extensibility:** The data layer is decoupled via EF Core entities (`Attraction`, `Destination`, `Category`), making it ready for integration with live third-party tourism APIs in future iterations.

