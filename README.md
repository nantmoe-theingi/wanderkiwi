# WanderKiwi 🥝

[![Live Frontend](https://img.shields.io/badge/Frontend-Live_App-0ea5e9?style=for-the-badge&logo=vercel&logoColor=white)](https://wanderkiwi-web-production.up.railway.app/)
[![Live Backend API](https://img.shields.io/badge/Backend-API_Swagger-10b981?style=for-the-badge&logo=dotnet&logoColor=white)](https://wanderkiwi-api-production.up.railway.app/)
[![Tech Stack](https://img.shields.io/badge/.NET_8_%7C_Angular-0f172a?style=for-the-badge&logo=angular&logoColor=white)](https://github.com/nantmoe-theingi/wanderkiwi)

WanderKiwi is a modern, full-stack travel planning web application designed to help users explore New Zealand through AI-powered recommendations, personalized custom itineraries, regional destination grids, and interactive travel guides.

---

## 🚀 Live Demo
* **Frontend Application:** [WanderKiwi Web App](https://wanderkiwi-web-production.up.railway.app/)
* **Backend API & Swagger:** [WanderKiwi REST API](https://wanderkiwi-api-production.up.railway.app/)

---

## 🛠️ Tech Stack
* **Backend:** .NET 8 Web API, Entity Framework Core, LINQ, SQL Server
* **Frontend:** Angular, TypeScript, SCSS
* **Routing & Navigation:** OpenRouteService (ORS) API
* **Cloud & DevOps:** Postgres SQL, Railway Deployment, GitHub Actions CI/CD

---

## ✨ Key Features
* **AI-Powered Trip Planner:** Custom trip generator allowing users to specify destinations, date ranges, traveler counts, trip styles, budget ranges, and personal interests to build day-by-day itineraries.
* **Saved Favourites:** Seamless bookmarking system leveraging `localStorage` and reactive state services (`FavoritesService`) allowing users to track and filter saved attractions.
* **Destination Explorer:** Comprehensive search and filtering interface supporting island toggles (North/South Island), regional filters, category tags (Nature, Adventure, Culture, Food & Wine), activity levels (Easy, Moderate, Challenging), and sorting options (Recommended, Rating, Name).
* **Travel Articles & Guides:** Curated articles section featuring travel tips, road trip advice, and "off the beaten path" highlights with responsive grid layouts.

---

## 🏗️ Architecture & Clean Code Highlights

WanderKiwi follows a robust, decoupled full-stack design separating the Angular single-page application from the .NET 8 Web API backend.

### Backend Structure (.NET Clean Architecture)
* **API Layer:** Handles HTTP requests, controllers, exception middleware, and endpoint routing.
* **Core / Domain Layer:** Contains enterprise business logic, domain models, Entity Framework entities (`Attraction`, `Destination`, `Category`), and service contracts.
* **Infrastructure Layer:** Manages database contexts, migrations, data seeding, and third-party API integrations (such as OpenRouteService).

### Frontend Structure (Angular Modular Design)
* **Component-Based Architecture:** Reusable standalone components for hero banners, navigation headers, filters, and dynamic itineraries.
* **Reactive State Management:** Utilizes Angular `Observable` streams and BehaviorSubjects (e.g., `FavoritesService`, `SearchService`) for cross-component state synchronization.
* **Modular SCSS Styling:** Fully responsive layouts built with customized SCSS styling, CSS Grid, and dynamic container scaling.

---

## 📦 Database Seeding & Mock Data
The project utilizes Entity Framework Core code-first migrations to seed structured tourism data into the database:
* **Scope:** Includes **50 verified points of interest** across major New Zealand regions (Queenstown, Christchurch, Auckland, and Te Anau) with precise road-accessible driving coordinates optimized for routing engines like **OpenRouteService (ORS)**.
* **Data Purpose:** Structured to support comprehensive mapping, navigation logic, card layout grids, and multi-property filter testing.

---

## ⚙️ Getting Started / Local Setup

To run this repository locally on your machine, follow these steps:

### Prerequisites
* .NET 8 SDK installed
* Node.js (v18+) and Angular CLI installed
* Postgres SQL Server 

### 1. Clone the Repository
```bash
git clone [https://github.com/nantmoe-theingi/wanderkiwi.git](https://github.com/nantmoe-theingi/wanderkiwi.git)
cd wanderkiwi
```

### 2. Run the Backend (.NET 8 Web API)
```bash
cd backend/Your.Backend.ProjectFolder
dotnet restore
dotnet ef database update
dotnet run
```
(The backend API will run locally, typically on https://localhost:7231 or http://localhost:5208)

### 3. Run the Frontend (Angular)
Open a separate terminal window:
```bash
cd frontend/wanderkiwi-web
npm install
ng serve
```
(Open your browser and navigate to http://localhost:4200)

## 🔌 External Integrations & API Note
* **OpenRouteService (ORS):** Integrated to compute geospatial mapping, routing logic, and travel distance data between attractions for the trip planner module.
* **Azure OpenAI & Weather APIs:** Orchestrated via the backend services to deliver intelligent itinerary planning context.

## 📄 License & Usage

This project is a portfolio demonstration project created by Nant Moe Theingi. It is intended for recruitment and evaluation purposes only.
* All source code and design assets are All Rights Reserved.
* You are welcome to view, explore, and review the code, but reproduction, redistribution, or commercial use without explicit permission is prohibited.
