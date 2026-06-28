# 🛒 E-Commerce Web API

A fully featured and scalable E-Commerce backend built with ASP.NET Core, Entity Framework Core, and Clean Architecture.

---

## 📖 Overview

This project is a production-ready E-Commerce Web API designed with scalability, clean architecture, and security in mind.
It provides core e-commerce functionality such as product management, authentication, cart operations, order handling, and payment processing — all built using enterprise-level design patterns.

This project helped me deepen my experience in building modern backend systems while applying best practices in architecture, security, caching, and performance optimization.

---

## 🚀 Technical Highlights

- **Clean Architecture** ensuring maintainability and separation of concerns
- **Entity Framework Core** with advanced querying & optimized data access
- **Redis Caching** for improving cart performance and reducing DB load
- **JWT Authentication** for secure, stateless API access
- **Stripe Payment Integration** for handling payment intents and order payments
- **Repository Pattern & Unit of Work** for clean data access
- **Specification Pattern** for reusable and testable queries
- **ASP.NET Core Identity** for authentication & role-based authorization
- **SQL Server** as a robust and scalable data store
- **Global Error Handling Middleware** for consistent error responses

---

## 🛠 Tech Stack

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Redis
- ASP.NET Core Identity
- Stripe.NET
- AutoMapper
- Swagger
- JWT Authentication
- Clean Architecture

---

## 🏗 Architecture

```
E-Commerce/
├── Core/
│   ├── Domain/                  # Entities, Contracts, Exceptions
│   └── Services.Abstraction/    # Service interfaces
├── Services/                    # Business logic implementations + Specifications
├── Infrastructure/
│   ├── Presention/              # Controllers
│   └── Presistence/             # DbContext, Repositories, Identity
├── Shared/                      # DTOs, Enums, Pagination
└── E-Commerce/                  # API entry point, Middlewares, Extensions
```

---

## 🔑 Key Features

**Products**
- Get all products with filtering, sorting, and pagination
- Filter by brand and type
- Specification pattern for flexible querying

**Authentication**
- Register & Login with JWT tokens
- Role-based authorization via ASP.NET Core Identity
- User profile & address management

**Basket**
- Redis-backed basket for fast read/write operations
- Add, update, and remove basket items

**Orders**
- Create orders from basket
- Track order status and payment status
- Delivery method selection

**Payments**
- Stripe payment intent creation
- Webhook support for payment confirmation

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Redis
- Stripe account (for payments)

### Setup

```bash
git clone https://github.com/MohamedKhaled2221/E-Commerce.git
cd E-Commerce
```

Update `appsettings.json` with your connection strings:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-sqlserver-connection-string",
    "Redis": "localhost"
  },
  "JwtOptions": {
    "SecretKey": "your-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience"
  },
  "StripeSettings": {
    "SecretKey": "your-stripe-secret-key",
    "PublishableKey": "your-stripe-publishable-key"
  }
}
```

```bash
dotnet ef database update
dotnet run --project E-Commerce
```

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
