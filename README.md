# <img width="64" height="64" alt="image" src="https://github.com/user-attachments/assets/3a9a63ab-87e7-4eb0-93a4-de6b20f3fc28" /> RUM – Anonymous Messaging App

Welcome to **RUM** (*Receive Unknown Messages*) — a secure, production-ready web app that lets users create accounts, generate public links, and receive anonymous messages, all while focusing on robust backend architecture.

I built this project to demonstrate my **backend development skills** in real-world scenarios including authentication, background processing, rate limiting, logging, and containerization — with clean, maintainable code and modern design patterns.

🔗 **Live Demo** (HTTPS Enabled):  
https://rum.runasp.net/

---

## 🧠 Why This Project?

This project reflects how I would design and implement a **secure, scalable, and maintainable** backend system in a real-world job environment.  


✅ Clean architecture using ASP.NET Core MVC  
✅ Cookie-based authentication for secure sessions  
✅ Public user links for receiving anonymous messages  
✅ Background service to auto-delete messages after 24 hours  
✅ Rate limiting to prevent spam and abuse  
✅ Structured logging using Serilog  
✅ Repository Pattern for maintainable data access  
✅ Docker-ready deployment for consistent environments  
✅ Free hosting with HTTPS enabled  

---


## Video Demo

[![Watch the video](https://img.youtube.com/vi/G24IneaZaUk/0.jpg)](https://youtu.be/G24IneaZaUk?feature=shared)

---

## 🛠️ Technologies Used

| Area             | Technology |
|------------------|------------|
| Language         | C# (.NET 9 SDK) |
| Framework        | ASP.NET Core MVC |
| Authentication   | Cookie-based authentication |
| Database         | SQLite (EF Core ORM) |
| Logging          | Serilog |
| Architecture     | Repository Pattern, Dependency Injection |
| Containerization | Docker |
| Hosting          | Free ASP.NET Hosting (HTTPS Enabled) |

---



**Prerequisites**  
- .NET SDK 9 or later  
- SQLite
- Docker (optional)

# Steps 

## Run locally
```bash
# Clone the repository
git clone https://github.com/mohamed-osman-se/RUM.git
cd RUM
dotnet restore
dotnet run
```

## Run with Docker
```bash
# Clone the repository
git clone https://github.com/mohamed-osman-se/RUM.git
cd RUM
docker build -t rum-app .
docker run -p 8080:80 rum-app

