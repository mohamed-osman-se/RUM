<p align="left">
  <img width="64" height="64" alt="RUM Logo" src="https://github.com/user-attachments/assets/3a9a63ab-87e7-4eb0-93a4-de6b20f3fc28" />
</p>

# RUM – Anonymous Messaging App

![.NET](https://img.shields.io/badge/.NET-9-blue)
![License](https://img.shields.io/badge/license-MIT-green)

RUM (Receive Unknown Messages) is a production-ready web application that allows users to create accounts, generate public links, and receive anonymous messages securely.

This project was built to demonstrate **real-world backend engineering practices**, focusing on clean architecture, security, background processing, and maintainability.

---

## Live Demo

**URL:** https://rum.runasp.net/

> **Note:**  
> The application is deployed on a **free trial hosting service**.  
> As a result, the app may occasionally be unavailable, partially functional, or experience cold starts due to hosting limitations.

---

## Project Motivation

The goal of this project is to showcase how I design and implement a **scalable, secure backend system** similar to what would be expected in a professional production environment.

Key focus areas:
- Authentication and session management
- Abuse prevention and rate limiting
- Background processing
- Clean and maintainable architecture
- Containerized deployment

---

## Core Features

- User registration and authentication (cookie-based sessions)
- Public profile links to receive anonymous messages
- Anonymous message submission without authentication
- Automatic message deletion after 24 hours using a background service
- Rate limiting to mitigate spam and abuse
- Structured logging for observability
- Repository pattern for clean data access
- Dockerized deployment for consistent environments

---

## Video Demo

[![Watch the video](https://img.youtube.com/vi/G24IneaZaUk/0.jpg)](https://youtu.be/G24IneaZaUk?feature=shared)

---

## Technology Stack

| Category           | Technology |
|--------------------|------------|
| Language           | C# (.NET 9 SDK) |
| Framework          | ASP.NET Core MVC |
| Authentication     | Cookie-based Authentication |
| Database           | SQLite with Entity Framework Core |
| Logging            | Serilog |
| Architecture       | Repository Pattern, Dependency Injection |
| Background Jobs    | Hosted Background Services |
| Containerization   | Docker |
| Hosting            | Free ASP.NET Hosting (HTTPS) |

---

## Prerequisites

- .NET SDK 9 or later  
- SQLite  
- Docker (optional)

---

## Running the Project Locally

```bash
git clone https://github.com/mohamed-osman-se/RUM.git
cd RUM
dotnet restore
dotnet run
```

## Running with Docker 

```bash
git clone https://github.com/mohamed-osman-se/RUM.git
cd RUM
docker build -t rum-app .
docker run -p 8080:80 rum-app
```

---

## Future Improvements & Contributions

The current implementation focuses on core backend functionality and fundamentals.  
Possible future improvements include:

- Publishing a pre-built Docker image on **Docker Hub** to simplify setup and deployment.
- Adding automated unit and integration tests.
- Introducing a **CI/CD pipeline using GitHub Actions** to automatically run tests and builds on each pull request.
- Enabling continuous deployment after successful validation.
- Improving monitoring and health checks for better observability.

These items are not part of the current scope and represent natural next steps.  
**Contributions are welcome via fork and pull request** to help implement these improvements.

