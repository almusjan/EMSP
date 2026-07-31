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
        EmployeeCost? matchingEmployeeCost = await _dbContext.EmployeeCosts.FirstOrDefaultAsync(ec => ec.Id == employeeCost.Id);

        if (matchingEmployeeCost == null)
            return employeeCost;

        #region CheckingUpdateFields

        matchingEmployeeCost.UpdatedAt = DateTime.UtcNow;
        
        matchingEmployeeCost.CostType =  employeeCost.CostType;
        matchingEmployeeCost.Description =  employeeCost.Description;
        matchingEmployeeCost.CostAmount = employeeCost.CostAmount;
        matchingEmployeeCost.DueDate = employeeCost.DueDate;
        matchingEmployeeCost.IsPaid = employeeCost.IsPaid;
        matchingEmployeeCost.PaidDate = employeeCost.PaidDate;
        matchingEmployeeCost.ReferenceNumber = employeeCost.ReferenceNumber;

        #endregion
        
        await  _dbContext.SaveChangesAsync();
        
        return matchingEmployeeCost;
    }
}