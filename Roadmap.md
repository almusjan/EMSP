# 📌 EMSP Project Roadmap

## 🚀 Phase 1: Core Foundation & Data Layer (✅ Completed)
- [x] Setup clean layered architecture (Api, Entities, Services, Repositories, ServiceContracts)
- [x] Configure `ApplicationDbContext` with proper relationships, `DeleteBehavior`, and indexes
- [x] Implement `HasPrecision(18, 2)` for all financial/decimal fields
- [x] Implement DTOs and clean Extension mapping methods
- [x] Add Data Annotations (`[Required]`, `[StringLength]`) to Entity models
- [x] Implement core Service & Repository methods for all entities (Employee, Company, Bank, Salary, etc.)

## 🧪 Phase 2: Testing & Query Optimization (✅ Completed)
- [x] Write comprehensive xUnit tests for Service layer business rules (using Moq, AutoFixture, FluentAssertions)
- [x] Optimize EF Core queries: Unified `GetAllAsync` and `GetFilteredAsync` using `Expression<Func<T, bool>>`
- [x] Ensure `!e.IsDeleted` and status filters are executed at the database level (no in-memory filtering)
- [x] Implement robust exception handling in Services (`ArgumentNullException`, `KeyNotFoundException`, `InvalidOperationException`)

## 🌐 Phase 3: API & Integration (⬜ In Progress / Next Up)
- [ ] Implement RESTful API Controllers for all core entities
- [ ] Map HTTP requests/responses to Service layer methods
- [ ] Implement Global Exception Handling Middleware (to catch `ValidationException`, `KeyNotFoundException`, etc., and return proper HTTP 400/404/500 responses)
- [ ] Add Swagger/OpenAPI documentation with XML comments (`/// <summary>`)
- [ ] Implement Model Validation at the API level (or integrate FluentValidation)

## 🛡️ Phase 4: Security & Production Readiness (⬜ Future)
- [ ] Implement JWT Authentication & Authorization (Roles: Admin, HR, Employee)
- [ ] Replace temporary audit placeholders with actual `UserId` (from `IHttpContextAccessor` / JWT claims) for `CreatedBy`/`UpdatedBy`
- [ ] Secure connection strings (use User Secrets in Dev, Environment Variables/Key Vault in Prod)
- [ ] Setup CI/CD pipeline (GitHub Actions) for automated build and xUnit testing on push

## 💡 Ideas / Backlog (⬜ Nice to Have)
- [ ] Add "Get Soft-Deleted Employees" endpoint (Admin only)
- [ ] Add "Hard Delete" endpoint for employees terminated for > 3 months
- [ ] Implement EF Core Global Query Filters (`modelBuilder.Entity<Employee>().HasQueryFilter(e => !e.IsDeleted)`) to auto-handle soft deletes
- [ ] Add pagination (`pageNumber`, `pageSize`) and dynamic sorting to `GetFilteredEmployeesAsync`
- [ ] Add automated DB seeding (`DbSeeder.cs`) for initial lookup data (Countries, Banks) on first launch

