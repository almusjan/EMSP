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
    
    public async Task<Salary> AddAsync(Salary salary)
    {
        salary.CreatedAt =  DateTime.UtcNow;
        salary.UpdatedAt = DateTime.UtcNow;
        
        await  _dbContext.Salaries.AddAsync(salary);
        await _dbContext.SaveChangesAsync();
        
        return salary;
    }

    public async Task<Salary> UpdateAsync(Salary salary)
    {
        Salary? matchingSalary = await _dbContext.Salaries.FirstOrDefaultAsync(s => s.Id == salary.Id);

        if (matchingSalary == null)
            return salary;
        
        #region CheckingUpdateFields

        matchingSalary.UpdatedAt = DateTime.UtcNow;
        
        matchingSalary.BasicSalary  = salary.BasicSalary;
        matchingSalary.HousingAllowance  = salary.HousingAllowance;
        matchingSalary.OtherAllowance =  salary.OtherAllowance;
        matchingSalary.TransportationAllowance = salary.TransportationAllowance;

        #endregion
        
        await _dbContext.SaveChangesAsync();
        
        return matchingSalary;
    }
}