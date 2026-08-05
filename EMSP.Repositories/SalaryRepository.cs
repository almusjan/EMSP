using EMSP.Entities;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSP.Repositories;

public class SalaryRepository : ISalaryRepository
{
    private readonly ApplicationDbContext _dbContext;
    public SalaryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Salary> AddAsync(Guid? employeeId, Salary salary)
    {
        if(employeeId.HasValue)
            salary.EmployeeId = employeeId;
        
        salary.CreatedAt =  DateTime.UtcNow;
        salary.UpdatedAt = DateTime.UtcNow;
        
        await  _dbContext.Salaries.AddAsync(salary);
        
        await _dbContext.SaveChangesAsync();
        
        return salary;
    }

    public Task<Salary?> GetByIdAsync(Guid? salaryId)
    {
        return _dbContext.Salaries.FirstOrDefaultAsync(s => s.Id == salaryId);
    }

    public async Task<Salary> UpdateAsync(Salary salary)
    {
        _dbContext.Salaries.Update(salary);
        _dbContext.Entry(salary).Property(s => s.UpdatedAt).CurrentValue = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
        
        return salary;
    }
}