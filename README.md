# Library Management System

## Overview

The **Library Management System** is built using **.NET Core**, providing a full solution for managing authors, books, borrowers, and book loans. This project follows a modular architecture with clear separation of concerns using modern technologies like **Entity Framework Core** for database management, **AutoMapper** for object mapping, **Dependency Injection (DI)** for managing dependencies, and comprehensive testing for business logic and data layers.

## Key Features

- **Author Management**: Full support for CRUD (Create, Read, Update, Delete) operations on authors.
- **Book Management**: Manage books, assign authors, and track availability.
- **Borrower Management**: Manage borrower details, including loan history.
- **Loan Management**: Track the lending and return of books.
- **Robust Validation**: Inputs are validated using **FluentValidation** or custom validation tools.
- **Object Mapping with AutoMapper**: Efficient conversion of objects between entities and DTOs.
- **Dependency Injection (DI)**: Use of DI for managing dependencies between classes, facilitating easier testing and centralized dependency management.
- **Entity Framework Core**: ORM for database management, supporting migrations and complex queries using LINQ.

## Core Technologies

1. **.NET Core**: The foundation of the application, providing a powerful platform for building Web APIs and microservices.
2. **Entity Framework Core**: ORM for database management, handling migrations, transactions, and LINQ-based querying.
3. **Dependency Injection (DI)**:
   - Built-in DI in ASP.NET Core to manage dependencies between classes and services.
   - Enables easy integration with repositories, services, and unit of work.
4. **AutoMapper**: A tool for mapping objects between different models (DTOs and Entities) for clean separation of concerns.
5. **Repository Pattern**: A repository pattern abstracts data access and CRUD operations for each entity, separating business logic from database operations.
6. **Unit of Work**: Manages transactions across multiple repositories to ensure that operations are executed atomically.
7. **FluentValidation**: A flexible and readable validation library for validating all entities, such as authors, books, borrowers, and loans.9. **SQL Server**: A choice of databases to manage relationships and entities within the system.

## System Architecture

This project follows a multi-layered architecture, ensuring the code is easy to maintain, extend, and test.

### Data Layer

- **Entity Framework Core** manages relationships between entities (authors, books, borrowers, and loans).
- Repositories handle data access logic and provide CRUD operations for each entity.
- The **Unit of Work** ensures all data operations occur within a single transaction.

### Service Layer

- The service layer contains all business logic, providing interfaces for complex CRUD operations.
- **Dependency Injection** injects services with necessary dependencies, including repositories, validators, and mappers using AutoMapper.
- It handles complex business transactions through services and the Unit of Work.

### Mapping Layer

- **AutoMapper** is used to convert objects between different layers (DTOs and entities) efficiently.
- This mapping ensures a clear separation of models used in different layers.

### Validation

- **FluentValidation** is used to validate inputs for all entities, including authors, books, borrowers, and loans.
- All input is validated before being persisted to ensure system integrity and reliability.

## Installation and Setup

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) version 3.1 or later.
- A supported database, such as **SQL Server** or **SQLite**.
- A development environment like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/yourusername/LibraryManagementSystem.git
    ```

2. Navigate to the project directory:

    ```bash
    cd LibraryManagementSystem
    ```

3. Restore dependencies:

    ```bash
    dotnet restore
    ```

4. Apply database migrations (if available):

    ```bash
    dotnet ef database update
    ```

### Running the Application

To run the application, use the following command:

```bash
dotnet run
```
## Future Enhancements

- **User Management and Role-Based Access**: Add user management with roles (admin, regular users) for access control.
- **Advanced Reporting**: Add detailed reports for overdue loans, borrowing trends, and more.
- **External API Integration**: Expose external APIs to integrate with third-party systems.

