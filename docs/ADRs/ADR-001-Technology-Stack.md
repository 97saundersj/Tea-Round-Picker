# Architecture Decision Record (ADR-001)

## Title: Technology Stack & Deployment for Nisien Tea Round Picker

### Context
Nisien Tea Round Picker is a productivity tool designed to facilitate fair tea selection in an office setting. It will:
- Provide an intuitive interface for managing teams and participants.
- Randomly select a tea maker.
- Record historical selections for transparency.

The application consists of two main components:
- **Frontend:** A React web application with TypeScript.
- **Backend:** An ASP.NET Core Web API in C#.

The project is organized into separate FrontEnd and BackEnd directories and is deployed on Azure, with the frontend hosted on Azure Static Web Apps and the backend on Azure App Service.

### Decision
The following technology stack and deployment approach have been chosen:
- **Frontend:** React with TypeScript
- **Backend:** ASP.NET Core Web API (C#)
- **Database:** Entity Framework (EF) using an in-memory database during development, with an easy transition to a persistent option (e.g., Azure SQL Database) planned.
- **Hosting/Deployment:**
  - **Frontend:** Azure Static Web Apps for efficient global distribution.
  - **Backend:** Azure App Service for reliable, scalable API hosting.
- **CI/CD:** Automated deployment via GitHub Actions.
- **Repository Structure:** Separate directories for `FrontEnd` and `BackEnd` ensure clear code boundaries.

### Rationale

#### **Frontend – React with TypeScript**
- **React:** A popular library for building dynamic user interfaces with reusable components.
- **TypeScript:** Provides static typing to improve code quality and reduce runtime errors.
- **Key Benefits:** Promotes a modular architecture, speeds up development with a rich ecosystem, and integrates seamlessly with REST APIs.

#### **Backend – ASP.NET Core Web API (C#)**
- **ASP.NET Core:** A proven framework for secure, high-performance APIs.
- **C#:** Offers robust tooling, asynchronous support, and strong testing capabilities.
- **Key Benefits:** Enables clear separation of business logic and data access, supports built-in security, and facilitates handling of complex algorithms.

#### **Azure Hosting & CI/CD**
- **Deployment:** Azure Static Web Apps and Azure App Service provide scalability and high availability.
- **CI/CD:** GitHub Actions streamline continuous integration and deployment, reducing manual overhead.

#### **Repository Structure**
- **Separation of Concerns:** Distinct directories for frontend and backend promote maintainability and collaboration.

### Consequences

#### **Positive Outcomes:**
- **Scalable Architecture:** The chosen stack supports growth and future enhancements.
- **Efficient Development:** Clear code separation and robust tooling enhance developer productivity.
- **Cloud-Ready:** Azure’s services offer scalability, reliability, and ease of management.
- **Improved Developer Experience:** Static typing and comprehensive API documentation (via Swagger) reduce errors and accelerate onboarding.

#### **Challenges:**
- **Initial Setup:** Configuring Azure services and production optimization for the .NET backend will require additional effort.
- **Database Transition:** Migrating from an in-memory database to a persistent one could introduce complexity.
- **Learning Curve:** While some team members may need time to familiarize themselves with TypeScript and .NET, extensive documentation and community support mitigate this challenge.

### Alternatives Considered

1. **Vue.js Frontend**
   - **Pros:** Familiarity with Vue.js could accelerate development.
   - **Cons:** Using React aligns with Nisien’s preferred framework, aiding review and interview discussions while providing a professional growth opportunity.

### Security Considerations
- **Authentication & Authorization:** ASP.NET Core provides built-in security features to protect APIs.
- **Data Protection:** Azure offers encryption, secure storage, and access controls to safeguard sensitive information.
- **Best Practices:** Secure API endpoints, enforce HTTPS, and implement role-based access control (RBAC) as needed.

### Decision Status
**Accepted** – The selected technology stack and deployment strategy, utilizing React with TypeScript and ASP.NET Core Web API (C#) hosted on Azure, are approved. Future iterations may incorporate persistent storage, enhanced analytics, and additional features such as notifications.

