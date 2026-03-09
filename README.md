# Inventory Sales Management System

Focused Technical Documentation (Important Details)

This document is intentionally focused on architecture, behavior, and maintenance-critical details.
It avoids exhaustive per-file listing and is intended to be used as a practical project README.

## 1) What the Project Is

Inventory Sales Management System is a desktop business application built with WinForms + .NET 9.
It manages:
- customers and workers
- products and product types
- inventory quantities
- invoices (sale/evaluation + refund flows)
- payments and balance effects
- audit logs for price changes and stock movements
- backup/restore of SQLite database

Primary language for UI and validation messages is Arabic.

## 2) High-Level Architecture

The repository follows a clear 3-layer architecture:

1. Presentation Layer (`InventorySalesManagementSystem`)
- WinForms screens, dialogs, user controls
- user interaction, event handling, binding
- calls service layer through dependency injection

2. Business Logic Layer (`LogicLayer`)
- service classes implement business rules and use-cases
- DTOs isolate UI contracts from EF entities
- validation orchestration and business exceptions

3. Data Access Layer (`DataAccessLayer`)
- EF Core `DbContext` + repositories + unit of work
- entity models and migrations
- persistence logic and query composition

Flow:
`WinForms -> Services -> Repositories/UnitOfWork -> EF Core -> SQLite`

## 3) Technology Stack

- Runtime: .NET 9 (`net9.0`, `net9.0-windows`)
- UI: Windows Forms
- ORM: Entity Framework Core 9
- Database: SQLite (`inventory.db`)
- Logging: Serilog file sinks
- DI/Config: `Microsoft.Extensions.DependencyInjection`, `ConfigurationBuilder`

Project files:
- `InventorySalesManagementSystem/InventorySalesManagementSystem.csproj`
- `LogicLayer/LogicLayer.csproj`
- `DataAccessLayer/DataAccessLayer.csproj`

## 4) Startup and Runtime Lifecycle

Main startup is in `InventorySalesManagementSystem/Program.cs`.

Important runtime behavior:

1. App paths
- Creates `%ProgramData%/InventorySales` directory
- Database path: `%ProgramData%/InventorySales/inventory.db`
- Logs path: `%ProgramData%/InventorySales/Logs`
- Local backup path: `%ProgramData%/InventorySales/LocalBackup`

2. Logging configuration
- `app-.log`: general app logs
- `ef-.log`: EF Core logs (excluding migrations)
- `migration-.log`: migration logs at warning+ level
- rolling daily logs with retention = 14 files

3. Config read
- Optional `appsettings.json` read from output directory
- `BackupSettings:SecondaryBackupPath` controls secondary backup directory
- default fallback in code: `D:\InventoryBackups`

4. DI registrations
- DbContext: SQLite
- UnitOfWork + generic repository + all specialized repositories
- all services (`CustomerService`, `ProductService`, `InvoiceService`, etc.)
- global singleton session state: `UserSession`
- singleton `BackupManager`

5. Migration and seed on startup
- checks pending migrations
- if migration needed and DB exists, pre-migration copy is created
- runs `db.Database.Migrate()`
- runs `DatabaseSeeder.Seed(db)`

6. Backup strategy
- backup attempted before entering main UI
- backup attempted again on shutdown (finally block)
- setting `LastBackupSucceded` is persisted in `Properties.Settings`

7. Login gate
- app opens `frmLogin`
- only on `DialogResult.OK` runs `FrmMain`

## 5) Persistence Model and DbContext

`DataAccessLayer/InventoryDbContext.cs` registers DbSets:
- `People`, `Towns`, `Customers`, `Workers`, `Users`
- `ProductTypes`, `Products`, `ProductPricesLog`, `ProductStockMovmentsLog`
- `Invoices`, `TakeBatches`, `SoldProducts`, `Payments`

Important model configuration:

- `Person.FullName` is computed column (SQLite expression concatenating name parts)
- Unique indexes:
  - `Town.TownName`
  - `ProductType.ProductTypeName`
- Search indexes:
  - `Person.FullName`
  - `Product.ProductName`
  - `Invoice.CustomerId`
  - `TakeBatch.InvoiceId`, `TakeBatch.TakeBatchType`
  - `SoldProduct.TakeBatchId`, `SoldProduct.ProductId`
  - `Payment.InvoiceId`, `Payment.CustomerId`, `Payment.PaymentReason`
- default values:
  - `Worker.IsActive = true`
  - `Customer.IsActive = true`
  - `Customer.Balance = 0`
- global FK delete behavior set to `Restrict`

## 6) Core Domain Objects

### Person / Town / Customer / Worker / User

- `Person` stores names, phone, national number, town link
- `Town` has unique name and is used by `Person`
- `Customer` references `Person` + has `Balance`, `IsActive`
- `Worker` references `Person` + has craft flags (`WorkersCraftsEnum`)
- `User` has username/password/isActive/permissions

Permission enum (`User.Permission`) uses flags:
- `Admin`, `View`, `Add`, `Edit`, `Delete`

### Product domain

- `ProductType`: category/model name
- `Product`: buying/selling prices, stock quantity, availability, type link
- `ProductPriceLog`: old/new buy/sell values + user + date
- `ProductStockMovementLog`: old/new quantity + reason + user + date + notes

Stock movement reasons:
- `Purchase`, `Sale`, `Adjustment`, `Damage`, `InitialStock`, `Refund`

### Invoice domain

- `Invoice`: open/close info, financial totals, discount, state/type, links
- `TakeBatch`: operation batch under invoice, includes sold product items
- `SoldProduct`: quantity and per-unit prices snapshot at operation time
- `Payment`: monetary transaction with reason and actor fields

Invoice enums:
- `InvoiceType`: `Evaluation` or `Sale`
- `InvoiceState`: `Open` or `Closed`

Take batch enum:
- `TakeBatchType`: `Invoice` or `Refund`

Payment reason enum:
- `Invoice`, `Refund`

## 7) Validation Strategy

Validation is layered:

1. Entity validation (`IValidatable.Validate(List<ValidationError>)`)
- almost all entities implement self-validation
- checks required fields, range rules, and logical constraints

2. Service-level logical validation
- operations enforce business state constraints (active customer, invoice state, etc.)

3. Format validation utilities
- under `LogicLayer/Validation` and `LogicLayer/Validation/Custom Validation`

4. Exception types used for control flow
- `ValidationException`
- `NotFoundException`
- `OperationFailedException`
- `WrongPasswordException`

## 8) Repository and Unit of Work Pattern

### Generic repository
`DataAccessLayer/Repos/Repository.cs`

Provides:
- `AddAsync`, `AddRangeAsync`, `Update`, `Delete`
- `GetByIdAsync`, `GetAllAsync`, paged `GetAllAsync`
- generic `GetTotalPagesAsync`

### Specialized repositories

Each aggregate has query-specific repositories, for example:
- `ProductRepository`: filtered product searches + include product type
- `InvoiceRepository`: deep include projections + summary groupings
- `PaymentRepository`: reason/date/customer filtering and summary projections

### Unit of Work
`DataAccessLayer/UnitOfWork.cs`

Provides:
- `SaveAsync()`
- `BeginTransactionAsync()`

Services use explicit transactions for multi-step aggregate operations.

## 9) Service Layer Responsibilities

### `CustomerService`
- CRUD for customer aggregate (`Customer + Person`)
- paging/search by full name / town
- activation toggle
- balance operations:
  - `DepositBalance`
  - `WithdrawBalance`

### `WorkerService`
- CRUD for worker aggregate (`Worker + Person`)
- paging/search by full name / town
- activation toggle

### `TownService`
- add/update/delete/list town data

### `UserService`
- validates login credentials (`username + password`)
- maps to `UserReadDto`

### `ProductTypeService`
- CRUD + paging/filter for product types

### `ProductService`
Critical behavior:
- add/update products in aggregate mode
- price-change writes price log records
- quantity add/remove enforces reason constraints
- stock changes always log old/new quantity + reason + user
- availability state can be toggled

### `ProductPriceLogService`
- add and query price logs
- supports name/date filtering

### `ProductStockMovementLogService`
- add and query stock movement logs
- supports name/date filtering

### `SoldProductService` + `TakeBatchService`
- build take-batch + sold-product aggregate from invoice input
- process sale/refund lines and quantity impacts

### `InvoiceService`
Important business rules:
- adding or appending batches updates invoice financial totals
- closing invoice is blocked unless remaining amount is exactly zero
- no normal sale batch modifications on closed invoices
- discounts are prevented when they would create invalid negative balance scenarios
- payment/refund operations validate customer active status and invoice type/state

### `PaymentService`
- creates payment records
- integrates with `InvoiceService`/`CustomerService` for financial consistency
- provides payment summaries and filtered paging queries

### Finance helper
`LogicLayer/Services/Helpers/InvoiceFinanceHelper.cs`

Core formulas:
- `NetSale = TotalSellingPrice - TotalRefundSellingPrice`
- `NetBuying = TotalBuyingPrice - TotalRefundBuyingPrice`
- `NetProfit = NetSale - NetBuying - Discount`
- `AmountDue = NetSale - Discount`
- `Remaining = AmountDue - TotalPaid`
- `RefundAmount = TotalPaid - AmountDue`

## 10) UI Structure (WinForms)

### Main shell
- `frmLogin`: credential entry and session initialization
- `FrmMain`: host panel and module navigation

### Worker screens
- list/add/update/show + `ucWorkerShow`

### Customer screens
- list/add/update/show + `ucCustomerShow`

### Product screens
- list/add/update/show + product type management
- price log list
- stock movement log list
- quantity-change info dialog

### Invoice screens
- invoice list (summary/management modes)
- add invoice, add batch, discount dialog
- invoice details and product summary
- refund summary
- sold-products user controls (`ucAddTakeBatch`, `ucProductSelector`, `ucInvoiceDetails`)

### Payment screens
- payments list
- add payment dialog
- invoice payment summary dialog

### Shared UI infrastructure
- `General/General Forms/frmBaseListScreen`
- `UserControles/UcListView`
- formatting helpers under `General/UiFormat.cs`

## 11) Authentication and Authorization Model

Current implementation:
- login checks exact username + exact password text equality
- authenticated user stored in singleton `UserSession`
- permission enum exists and helper extension converts flags to display text

Important: password storage/check is currently plain-text comparison.

## 12) Backup and Recovery Details

Backup component:
`DataAccessLayer/backup/BackupManager.cs`

Behavior:
- synchronized via static lock
- SQLite-native online backup (`SqliteConnection.BackupDatabase`)
- writes to local backup folder and optional secondary folder
- keeps newest N backups (`keepBackups`)
- emergency fallback copy to `D:\InventoryBackups` when primary backup flow fails
- `RestoreBackup` creates pre-restore backup best effort, then replaces DB file

App setting involved:
- `InventorySalesManagementSystem/appsettings.json`

User setting involved:
- `InventorySalesManagementSystem/App.config`
- `Properties.Settings.Default.LastBackupSucceded`

## 13) Migrations and Seeding

Migrations live in `DataAccessLayer/Migrations`.

Design-time helpers:
- `DataAccessLayer/DesignTimeOnly/InventoryDbContextFactory.cs`
- `DataAccessLayer/DesignTimeOnly/DatabaseSeeder.cs`

Seeder behavior:
- ensures at least one town (`"عام"`)
- ensures admin user exists:
  - username: `admin`
  - password: `123456`
  - permissions: `Admin`

## 14) Query and Paging Conventions

Common conventions in repositories/services:
- paging is 1-based input (`PageNumber`, `RowsPerPage`)
- page count computed via `Ceiling(totalCount / rowsPerPage)`
- list queries usually `AsNoTracking()`
- default order in many lists is latest first by date or ascending by id
- text searches typically use `StartsWith` or `Contains`

## 15) Error Handling and Logging Pattern

Service pattern:
- validate inputs and state first
- throw typed business exceptions for user-facing failures
- wrap data mutation in transaction where operation spans multiple entities
- log with context and rethrow `OperationFailedException` when needed

App-level pattern:
- catches migration/startup fatal errors in `Program.cs`
- catches top-level runtime crash and flushes logs

## 16) Important Implementation Notes and Risks

1. Security: passwords are plain text
- both seeded and checked without hashing
- login error logging currently includes username and password fields

2. Domain integrity logic is service-dependent
- several invariants rely on service orchestration, not DB constraints

3. Naming/typo consistency
- there are minor naming typos in identifiers (e.g., `RecivedBy`, some method names)
- does not break function but affects maintainability and API cleanliness

4. UI and service coupling by direct DI scope usage
- forms often create scopes in event handlers
- practical for desktop app, but can complicate lifetime tracing

5. Secondary backup path reliability
- depends on configured path availability and permissions

## 17) How to Run (Developer)

Prerequisites:
- .NET SDK 9.x
- Windows (WinForms target)

Steps:
1. Restore and build solution.
2. Run `InventorySalesManagementSystem` project.
3. On first run, app creates database and applies migrations.
4. Login with seeded admin if DB is fresh:
   - username `admin`
   - password `123456`

## 18) Suggested Next Engineering Improvements

Priority order:

1. Replace plain-text passwords with salted hash (e.g., PBKDF2/Argon2/Bcrypt).
2. Remove sensitive login data from logs.
3. Add automated tests for key finance invariants (invoice/pay/refund/discount).
4. Introduce explicit transactional boundaries in all cross-aggregate mutations.
5. Add optimistic concurrency tokens on critical entities (`Invoice`, `Product`, `Customer`).
6. Normalize naming typos and public method consistency.
7. Add role-based permission checks in UI actions and service entry points.

## 19) Project Layout (Condensed)

- `InventorySalesManagementSystem/`
  - WinForms app, forms, user controls, resources, startup config
- `LogicLayer/`
  - DTOs, services, validation helpers, utility extensions, user session
- `DataAccessLayer/`
  - entities, repositories, abstractions, DbContext, unit of work, migrations, backup manager

This is the primary maintenance map for onboarding and feature extension.
