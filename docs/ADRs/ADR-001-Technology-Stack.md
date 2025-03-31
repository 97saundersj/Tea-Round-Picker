# Architecture Decision Record (ADR) – ADR-001

## Title: Technology Stack & Deployment Decisions for Nisien Tea Round Picker

### Context
The Nisien Tea Round Picker is a productivity tool designed to facilitate fair tea selection in an office setting. The application will:
- Provide a user-friendly interface for managing teams, participants, and viewing selection results.
- Select a tea maker randomly.
- Include features for saving and retrieving past selections for transparency.

The project consists of two primary components:
- **Frontend:** A React web application built with TypeScript.
- **Backend:** A .NET Web API built using C#.

The application is deployed to Azure, with the frontend hosted on Azure Static Web Apps and the backend on Azure App Service. The repository is organized with separate `FrontEnd` and `BackEnd` directories for clear separation of client and server code.

### Decision
I have chosen the following technology stack and deployment strategy:
- **Frontend:** React with TypeScript
- **Backend:** ASP.NET Core Web API (C#)
- **Database:** Entity Framework (EF) with an in-memory database during development - EF will allow me to easily expand the project to a persistent database (e.g., Azure SQL Database) at a later time.
- **Hosting/Deployment:**
  - **Frontend:** Azure Static Web Apps
  - **Backend:** Azure App Service
- **Repository Structure:** The codebase is divided into `FrontEnd` and `BackEnd` directories for clear organization.

### Rationale

#### **Frontend – React with TypeScript**
- **React:** A modern, widely-used JavaScript framework that enables dynamic, responsive UIs with reusable components.
- **TypeScript:** Adds static typing to JavaScript, enhancing developer experience and reducing runtime errors.
- **Benefits:**
  - Encourages modularity and component-based architecture.
  - React's strong ecosystem and library support (e.g., React Bootstrap) speed up UI development.
  - Easy integration with backend REST APIs.

#### **Backend – ASP.NET Core Web API (C#)**
- **ASP.NET Core:** A robust, scalable framework for building secure, high-performance APIs.
- **C#:** The language of choice for backend development, providing strong tooling, support for asynchronous operations, and integrated testing capabilities.
- **Benefits:**
  - Well-suited for building RESTful APIs with clear separation between data access and business logic.
  - Built-in security features such as authentication and authorization.
  - Excellent tooling and debugging support in the .NET ecosystem.
  - Capable of handling complex algorithms for selection logic.

#### **Hosting/Deployment on Azure**
- **Frontend:** Hosted on Azure Static Web Apps for simple deployment and global distribution.
- **Backend:** Deployed via Azure App Service, providing scalability and reliability for the API.
- **CI/CD:** Azure supports continuous integration and deployment through GitHub Actions, ensuring smooth and automated deployments.

#### **Repository Structure**
- **FrontEnd and BackEnd Directories:** Code is separated into distinct directories to maintain clear boundaries between client and server code.
- **Modular Design:** This structure allows easy maintenance and scalability of both frontend and backend components.

### Consequences

#### **Positive Outcomes:**
- **Modern, Scalable Architecture:** The chosen stack aligns with current development best practices, providing a foundation that can scale as new features are added.
- **Clear Separation of Concerns:** Keeping frontend and backend in separate directories simplifies the development process and makes the project easier to maintain.
- **Cloud-Readiness:** Hosting the app on Azure ensures scalability, high availability, and ease of management through cloud-native services.
- **Developer Experience:** TypeScript adds static typing to JavaScript, improving code quality and reducing errors. Swagger documentation helps developers understand and interact with the backend API more easily.

#### **Challenges:**
- **Initial Setup Complexity:** Setting up Azure services and configuring the .NET backend for production deployment may require additional setup time.
- **Database Integration:** If persistent storage is needed in future versions (e.g., to store preferences or analytics), integrating a database like PostgreSQL may add complexity.
- **Learning Curve:** Developers unfamiliar with TypeScript or the .NET ecosystem might face a learning curve, although the strong documentation and community support mitigate this.

### Alternatives considered

1. **Vue.js Frontend**
   - *Pros:* I have past experience developing with Vue.js, which could lead to faster development and a more intuitive design process.
   - *Cons:* While Vue.js is a powerful framework, I have always wanted to use React to build a web app so I thought this was a good opertunity to learn and challange myself, especially since React is the framework used by Nisien, meaning that it should be easier for them to review and to (hopefully) discuss during the interview process.

### Decision Status
**Accepted** – The current stack and deployment strategy using React with TypeScript for the frontend and ASP.NET Core Web API (C#) for the backend, hosted on Azure, will be maintained. Future iterations may include persistent storage solutions or additional features like fairness analytics or notifications.