using System.Linq.Expressions;
using EMSP.Entities;
using EMSP.Entities.Enums;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSP.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext  _dbContext;
    
    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<Employee> GetQueryableEmployee()
    {
        return _dbContext.Employees
            .Include(e => e.Establishment)
            .Include(e => e.Company);
    }
    
    public async Task<List<Employee>> GetAllAsync() =>
        await GetQueryableEmployee().ToListAsync();

    public async Task<List<Employee>> GetFilteredAsync(Expression<Func<Employee, bool>> predicate) =>
        await GetQueryableEmployee().Where(predicate).ToListAsync();

    public async Task<Employee?> GetByIdAsync(Guid? employeeId)
    {
        return await GetQueryableEmployee()
            .Include(e => e.Country)
            .Include(e => e.HealthInsurance)
            .Include(e => e.Salary)
            .Include(e => e.Bank)
            .Include(e => e.EmployeeCosts)
            .FirstOrDefaultAsync(e => e.Id == employeeId);
    }


    public async Task<bool> IsIqamaExistsAsync(string iqamaOrIdNumber) =>
        await _dbContext.Employees.AnyAsync(e => e.IqamaOrIdNumber == iqamaOrIdNumber);

    public async Task<Employee> AddAsync(Employee employee)
    {
        employee.CreatedAt = DateTime.UtcNow;
        employee.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.Employees.AddAsync(employee);
        await  _dbContext.SaveChangesAsync();
        
        return employee;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        _dbContext.Employees.Update(employee);
        _dbContext.Entry(employee).Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
        
        return employee;
    }
}