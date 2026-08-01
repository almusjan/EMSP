using EMSP.Entities;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSP.Repositories;

public class EmployeeCostRepository : IEmployeeCostRepository
{
    private readonly ApplicationDbContext _dbContext;
    public EmployeeCostRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<EmployeeCost?> GetByIdAsync(Guid? employeeCostId) => 
        await _dbContext.EmployeeCosts.FirstOrDefaultAsync(ec => ec.Id == employeeCostId);

    public async Task<EmployeeCost> AddAsync(EmployeeCost employeeCost)
    {
        employeeCost.CreatedAt =  DateTime.UtcNow;
        employeeCost.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.EmployeeCosts.AddAsync(employeeCost);
        await _dbContext.SaveChangesAsync();
        
        return employeeCost;
    }

    public async Task<EmployeeCost> UpdateAsync(EmployeeCost employeeCost)
    {
        _dbContext.EmployeeCosts.Update(employeeCost);
        _dbContext.Entry(employeeCost).Property(ec => ec.UpdatedAt).CurrentValue = DateTime.UtcNow;
        
        await  _dbContext.SaveChangesAsync();
        
        return employeeCost;
    }
}