MenuMaster-Backend
==============

**MenuMaster** is a RESTful backend API designed for restaurant and menu management. It provides user authentication, restaurant listings, menu management, reservations, and review functionality.

This project was developed as a **graduation thesis backend application**.

🚀 Technologies Used
--------------------

*   .NET 8 (ASP.NET Core Web API)
*   Entity Framework Core (Code First)
*   Azure SQL / SQL Server
*   JWT Authentication
*   Swagger (OpenAPI)
*   Docker (for local database setup)

🏗 Architecture Overview
------------------------

The project follows a layered architecture:

*   **Controllers** – Handle HTTP requests
*   **Services** – Business logic layer
*   **Repositories** – Data access layer
*   **Database** – EF Core with migrations

Authentication is handled using JWT tokens.

⚙️ Requirements
---------------

*   .NET 8 SDK
*   Docker (optional, for local SQL Server)
*   SQL Server or Azure SQL

🛠 Setup & Run
--------------

### 1️⃣ Clone the repository

git clone <repository-url>
cd MenuMaster

### 2️⃣ Configuration

Sensitive configuration values are not included in the repository. Use `appsettings.sample.json` as a reference and provide actual values via environment variables or local configuration.

### 3️⃣ Database setup (Docker)

docker compose up -d

Apply database migrations:

dotnet ef database update

### 4️⃣ Run the application

dotnet run

Swagger UI will be available at: `http://localhost:5000/swagger`

🔐 Authentication
-----------------

The API uses JWT-based authentication.

1.  Authenticate using the login endpoint
2.  Copy the returned JWT token
3.  Open Swagger and click **Authorize**
4.  Enter the token in the format:

Bearer <your\_token>

📧 Email Feature
----------------

The email service is **disabled by default** for security and demo purposes.

To enable email functionality:

"Email": {
  "Enabled": true
}
    

SMTP credentials must be provided via secure configuration methods.

📄 Configuration Notes
----------------------

*   Sensitive data (database credentials, JWT keys, email passwords) are excluded from the repository.
*   `appsettings.sample.json` is provided as a reference.
*   Actual values should be provided via environment variables or local configuration.

📌 Notes
--------

This project was developed for academic purposes and demonstrates backend API development, authentication, and clean architecture principles.