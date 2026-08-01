# 📌 EMSP Project Roadmap

## 🚀 Phase 1: Core Foundation (In Progress)
- [x] Setup layered architecture (Api, Entities, Services, Repositories)
- [x] Configure `ApplicationDbContext` with proper relationships and precision
- [x] Implement DTOs and Extension mapping methods
- [x] Implement remaining Service methods (Company, Bank, Salary, etc.)
- [ ] Add Data Annotations / FluentValidation to all Request DTOs

## 🛡️ Phase 2: Security & Robustness
- [ ] Implement JWT Authentication & Authorization
- [ ] Replace `"System"` placeholder with actual `UserId` for `CreatedBy`/`UpdatedBy`
- [ ] Add Global Exception Handling Middleware in `EMSP.Api`
- [ ] Write xUnit tests for all Service layer business rules (Target: 80% coverage)

## 🧹 Phase 3: Polish & Optimization
- [ ] Review and optimize EF Core queries (ensure `.Select()` projection is used for lists)
- [ ] Add Swagger documentation comments (`/// <summary>`) to API controllers
- [ ] Setup CI/CD pipeline (GitHub Actions) for automated testing

## 💡 Ideas / Backlog
- [ ] Add "Get Soft-Deleted Employees" endpoint
- [ ] Add "Hard Delete" endpoint for employees terminated > 3 months
- [ ] Add pagination and sorting to `GetFilteredEmployeesAsync`