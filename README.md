# </> Code Book

## Description
**A developer-focused social platform for sharing code, collaborating, and building community.**

## 🌐 Live Demo
**[https://code-book-e6e0cdeke2cfgkgz.polandcentral-01.azurewebsites.net/HTML/HomePage.html]**

> Hosted on **Azure App Service** — Poland Central region.
## 🧭 Overview
CodeBook is a full-stack web application built with **ASP.NET MVC (C#)** on the backend and **Vanilla HTML/CSS/JavaScript** on the frontend. It enables developers to:

- Share code snippets with syntax highlighting and language tagging
- Comment, react, and engage with other developers' posts
- Follow other developers and get notified of their activity
- Join public and private communities around specific technologies
- Search and filter posts by language, tag, or community
- Report and moderate content through an admin dashboard

---
## ✨ Features

| Feature | Description |
|---|---|
| 📝 **Code Posts** | Share code snippets with title, body, language tag, and public/private visibility |
| 💬 **Threaded Comments** | Nested reply system using self-referencing `ParentCommentId` |
| ❤️ **Reactions** | Like, Love, Laugh reactions on both posts and comments |
| 🔔 **Notifications** | Real-time alerts for comments, reactions, follows, and mentions |
| 👥 **Communities** | Public and private groups |
| 🔍 **Search & Filter** | Search by title, language, tag, or community with debounced input |
| 💾 **Saved Posts** | Bookmark posts for later access |
| 🔒 **Full Auth Flow** | Register, login, logout |
| 🛡️ **Admin Moderation** | Report queue, post removal with full audit trail, role-based access |
| 👤 **User Profiles** | Avatar, bio, follower/following counts, follow/unfollow |

---
## 🏗️ Architecture

CodeBook follows a strict **3-layer architecture**:

```
┌─────────────────────────────────────────────┐
│              Frontend (HTML/CSS/JS)          │
│         Vanilla JS · Fetch API · api.js      │
└──────────────────┬──────────────────────────┘
                   │ HTTP + HttpOnly Cookies
┌──────────────────▼──────────────────────────┐
│           Layer 3 — API Layer               │
│   ASP.NET Controllers · Middleware Pipeline  │
│   JWT Auth · ApiResponse<T> · Swagger        │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│        Layer 2 — Business Logic Layer        │
│  Services · FluentValidation · AutoMapper    │
│  Domain Events · Notifications               │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│        Layer 1 — Data Access Layer           │
│  EF Core · Code First · Fluent API           │
│  Generic Repository · Unit of Work           │
│                      SQL Server              │
└──────────────────┬──────────────────────────┘
                   │
           ┌───────▼────────┐
           │   Azure SQL DB  │
           └────────────────┘
```
## 🛠️ Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| ASP.NET API (C#) | Web framework |
| Entity Framework Core 8 | ORM |
| Fluent API (Code First) | Entity mapping & configuration |
| Microsoft SQL Server | Database |
| AutoMapper | Object-to-object mapping |
| FluentValidation | Request DTO validation |
| Swagger | API documentation |

### Authentication & Security
| Technology | Purpose |
|---|---|
| JWT Bearer | Access token generation & validation |
| HttpOnly Cookies | Secure token storage (XSS-safe) |
| BCrypt | Password hashing |
| RBAC | Role-based access control (`[Authorize(Policy="AdminOnly")]`) |

### Frontend
| Technology | Purpose |
|---|---|
| HTML5 | Page structure |
| CSS3 | Styling Pages |
| Vanilla JavaScript (ES6) | Interactivity |
| Fetch API | HTTP communication via shared `api.js` wrapper |

### DevOps & Infrastructure
| Technology | Purpose |
|---|---|
| Azure App Service | Hosting (Poland Central, Linux, B1) |
| Azure SQL Database | Cloud database |
| GitHub Actions | CI/CD pipeline |
| Azure Application Insights | Monitoring & telemetry |

---
## 📁 Project Structure

```
CodeBook/
├── CodeBook.API.APP/                  # Layer 3 — API Layer
│   ├── Controllers/
│   ├── Program.cs
│   ├── DependencyInjection.cs
│   └── wwwroot/                   # Frontend static files
│       ├── HTML/
│       ├── css/
│       │   └── styles.css
│       └── js/
│           ├── api.js             # Shared fetch wrapper
│           └── ...
│
├── CodeBook.Business.App/             # Layer 2 — Business Logic
│   ├── Services/
│   ├── Interfaces/
│   ├── Validators/
│   ├── DTOs/
│   └── Mapping/
│       └── MappingProfile.cs
│
├── CodeBook.Data.App/                 # Layer 1 — Data Access
│   ├── CodeBookContext.cs
│   ├── Repositories/
│   ├── IRepositories/
│   └── Migrations/
│
└── CodeBook.Models.App/                 # Entities
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or [Azure SQL](https://azure.microsoft.com/en-us/products/azure-sql/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

### Local Setup

**1. Clone the repository**
```bash
git clone https://github.com/your-org/codebook.git
cd codebook
```

**2. Configure `appsettings.Development.json`**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CodeBookDb;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "your-local-secret-key-min-32-chars",
    "Issuer": "CodeBook",
    "Audience": "CodeBookUsers"
  }
}
```

**3. Run migrations**
```bash
cd CodeBook.Data.App
dotnet ef database update --startup-project ../CodeBook.API.App
```

**4. Run the application**
```bash
cd CodeBook.API.App
dotnet run
```

**5. Open in browser**
```
https://localhost:44313
https://localhost:44313/swagger   ← API docs
```

### Frontend Development

The frontend runs from `wwwroot/` served by the same ASP.NET process. Open `wwwroot/HTML/HomePage.html` via the running server — no separate dev server needed.

For local frontend-only testing with Live Server (VS Code extension), update `api.js`:
```javascript
const API_BASE = 'https://localhost:44313/api/v1';
```

---
## 👥 Developers

- Habiba Ahmed
- Roaa Bahaa
- Malak Yousry
- Farida Yousry
---
