# Inventory Sales Management System

A full-featured **Inventory and Sales Management desktop application** built with **.NET 9.0**, designed to manage products, sales, invoices, payments, customers, and stock movement with complete financial traceability.

The system follows a **Three-Tier Architecture (N-Tier)** that cleanly separates the user interface, business logic, and database operations to ensure maintainability, scalability, and code clarity.

The application is designed primarily for **Arabic-speaking businesses**, with localized labels and validation messages.

---

# Table of Contents

1. Overview
2. Features
3. System Architecture
4. Technology Stack
5. Project Structure
6. Domain Model
7. Data Access Layer
8. Business Logic Layer
9. Presentation Layer
10. Database Design
11. Audit & Logging System
12. Authentication & Permissions
13. Error Handling
14. Installation & Setup
15. Running the Application
16. Database Migrations & Seeding
17. Backup System
18. Future Improvements
19. License

---

# 1. Overview

Inventory Sales Management System is a desktop application used to manage:

* Product inventory
* Sales invoices
* Customer balances
* Payments
* Product pricing history
* Stock movements

The system ensures **complete auditability** of financial and inventory operations by logging every price change and stock movement.

This design prevents data inconsistencies and allows accurate historical reporting.

---

# 2. Features

### Inventory Management

* Create and manage products
* Organize products by type
* Track product pricing history
* Track stock movement
* Manage product batches

### Sales Management

* Create sales invoices
* Add multiple products to invoices
* Apply invoice discounts
* Handle refunds
* Track invoice financial summaries

### Customer Management

* Add and manage customers
* Track customer balances
* View customer purchase history

### Payments

* Record payments from customers
* Apply payments to invoices
* Maintain financial balance integrity

### Worker & User Management

* Manage employees
* Create system users
* Authentication and permission management

### Audit Logging

* Product price history logging
* Inventory movement tracking
* Financial traceability

### Database Backup

* Manual and automated backup system
* Built-in backup management interface

---

# 3. System Architecture

The system follows a **Three-Tier Architecture** to enforce separation of concerns.

```
Presentation Layer (WinForms UI)
        ↓
Business Logic Layer (Services)
        ↓
Data Access Layer (EF Core + Repositories)
        ↓
SQL Database
```

### Advantages of this architecture

* Better maintainability
* Easier testing
* Decoupled components
* Clear responsibility boundaries

---

# 4. Technology Stack

| Component    | Technology                                    |
| ------------ | --------------------------------------------- |
| Framework    | .NET 9.0                                      |
| Language     | C#                                            |
| UI Framework | Windows Forms                                 |
| ORM          | Entity Framework Core                         |
| Database     | SQL Server                                    |
| Architecture | Three-Tier (N-Tier)                           |
| Patterns     | Repository Pattern, Unit of Work, DTO Pattern |

---

# 5. Project Structure

```
Solution
│
├── DataAccessLayer
│   ├── Entities
│   ├── Repositories
│   ├── Interfaces
│   ├── Migrations
│   ├── BackupManager
│   └── InventoryDbContext
│
├── LogicLayer
│   ├── DTOs
│   ├── Services
│   ├── Exceptions
│   ├── Helpers
│   └── Utilities
│
└── InventorySalesManagementSystem
    ├── Forms
    ├── UserControls
    ├── Resources
    ├── Configuration
    └── Program.cs
```

---

# 6. Domain Model

The system models the following core business entities.

### Person

Base representation of any human in the system.

Properties include:

* First Name
* Second Name
* Third Name
* Fourth Name
* Full Name
* National Number
* Phone Number
* Town

### Customer

Represents clients who purchase products.

Key attributes:

* Balance
* Active status
* Purchase history

### Worker

Represents employees working in the system.

### User

Represents application users who can log into the system.

Includes authentication credentials and permissions.

### Product

Represents an item in the inventory.

Attributes include:

* Name
* Barcode
* Product Type
* Price
* Stock quantity

### ProductType

Represents product categories.

### Invoice

Represents a completed sales transaction.

### SoldProduct

Represents line items within an invoice.

### TakeBatch

Represents product batches removed from inventory during sales.

### Payment

Represents financial transactions received from customers.

---

# 7. Data Access Layer

The **Data Access Layer (DAL)** is responsible for interacting with the database using **Entity Framework Core**.

## InventoryDbContext

Defines the database schema using DbSet properties.

Example:

```
DbSet<Person>
DbSet<Customer>
DbSet<Product>
DbSet<Invoice>
DbSet<SoldProduct>
DbSet<Payment>
```

### Fluent API Configuration

Used to configure:

* Default values
* Relationships
* Constraints
* Keys

Example configurations:

* Worker.IsActive → default true
* Customer.IsActive → default true
* Customer.Balance → default 0

---

# 8. Repository Pattern

The system uses a **Generic Repository Pattern** to abstract database operations.

### IRepository<T>

Provides common operations:

* Add
* GetById
* GetAll
* Update
* Delete

### Repository<T>

Concrete implementation using EF Core.

### Specific Repositories

Custom repositories provide domain-specific queries.

Examples:

* CustomerRepository
* ProductRepository
* InvoiceRepository

---

# 9. Unit Of Work

The **Unit of Work pattern** ensures that multiple database operations are committed as a single transaction.

Example scenario:

Creating an invoice requires:

1. Creating invoice record
2. Adding sold products
3. Deducting stock
4. Updating customer balance
5. Logging stock movement

All operations are executed within a single transaction.

If any step fails, the transaction is rolled back.

---

# 10. Business Logic Layer

The **Business Logic Layer (BLL)** contains the core business rules.

It acts as a mediator between the UI and the data layer.

---

## Services

The system contains several service classes responsible for implementing business rules.

### CustomerService

Handles:

* Creating customers
* Updating balances
* Retrieving customer data

### ProductService

Handles:

* Product management
* Inventory operations

### InvoiceService

The most critical component of the system.

Responsibilities include:

* Creating invoices
* Calculating totals
* Applying discounts
* Managing product stock deductions
* Logging stock movements

### PaymentService

Handles customer payment processing.

---

# 11. Data Transfer Objects (DTOs)

DTOs are used to safely transfer data between layers.

Each entity has multiple DTO models depending on the operation.

Example:

```
CustomerAddDto
CustomerUpdateDto
CustomerReadDto
CustomerListDto
```

Benefits:

* Prevents over-posting
* Improves security
* Optimizes data transfer

---

# 12. Audit & Logging System

The system includes a comprehensive audit system.

### ProductPriceLog

Tracks every product price change.

Example fields:

* ProductId
* OldPrice
* NewPrice
* ChangedDate
* ChangedBy

### ProductStockMovementLog

Tracks all stock movements.

Records:

* Stock additions
* Sales deductions
* Inventory adjustments

This ensures full traceability.

---

# 13. Authentication & Permissions

The system includes user authentication.

Users must log in through the **Login Form**.

After successful login:

* User session is stored in `UserSession`
* Permissions are applied across the system

Permission utilities are handled using:

```
PermissionsExtensions
EnumExtensions
```

---

# 14. Error Handling

The system uses custom exception types.

Examples:

```
ValidationException
NotFoundException
OperationFailedException
WrongPasswordException
```

These provide meaningful error handling throughout the application.

---

# 15. Presentation Layer

The Presentation Layer is built using **Windows Forms**.

It provides the graphical interface for interacting with the system.

### Core Forms

* Login Form
* Main Dashboard
* Customer Management Screens
* Product Management Screens
* Invoice Management Screens
* Payment Screens

### User Controls

Reusable UI components are used for common forms such as:

* Person data entry
* Product display
* Customer display

---

# 16. Installation & Setup

### Requirements

* .NET 9 SDK
* SQL Server
* Visual Studio 2022 or later

### Steps

1. Clone the repository.

```
git clone https://github.com/your-repo/InventorySalesManagementSystem.git
```

2. Open the solution in Visual Studio.

3. Update the connection string in:

```
appsettings.json
or
App.config
```

4. Run database migrations.

```
Update-Database
```

5. Start the application.

---

# 17. Database Migrations

The project includes EF Core migrations that track database schema changes.

Example migration:

```
20260204164435_NewInitialCommit
```

To apply migrations:

```
Update-Database
```

---

# 18. Database Seeding

Initial data can be inserted using the database seeder.

Examples of seeded data:

* Default admin user
* Initial towns
* Product categories

Seeder located in:

```
DesignTimeOnly/DatabaseSeeder.cs
```

---

# 19. Backup System

The system includes a built-in **database backup manager**.

Capabilities include:

* Manual backups
* Scheduled backups
* Backup restoration

The backup UI is available through:

```
frmBackup
```

---

# 20. Future Improvements

Possible enhancements include:

* Migration to WPF or Web interface
* REST API integration
* Mobile client support
* Advanced reporting system
* Real-time inventory dashboards

---

# 21. License

This project is intended for educational and business use.

License details should be added according to project requirements.
