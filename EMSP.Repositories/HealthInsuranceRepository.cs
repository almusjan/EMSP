using EMSP.Entities;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSP.Repositories;

public class HealthInsuranceRepository : IHealthInsuranceRepository
{
    private readonly ApplicationDbContext _dbContext;
    public HealthInsuranceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<HealthInsurance> AddAsync(HealthInsurance healthInsurance)
    {
        healthInsurance.CreatedAt =  DateTime.UtcNow;
        healthInsurance.UpdatedAt = DateTime.UtcNow;
        
        _dbContext.HealthInsurances.Add(healthInsurance);
        await _dbContext.SaveChangesAsync();
        
        return healthInsurance;
    }

    public async Task<List<HealthInsurance>> GetAllAsync()
    {
        return await _dbContext.HealthInsurances.ToListAsync();
    }

    public async Task<HealthInsurance?> GetByIdAsync(Guid? healthInsuranceId)
    {
        return await _dbContext.HealthInsurances
            .Include(hi => hi.Establishment)
            .Include(hi => hi.Employees)
            .FirstOrDefaultAsync(hi => hi.Id == healthInsuranceId);
    }

    public async Task<HealthInsurance> UpdateAsync(HealthInsurance healthInsurance)
    {
        _dbContext.HealthInsurances.Update(healthInsurance);
        _dbContext.Entry(healthInsurance).Property(hi => hi.UpdatedAt).CurrentValue = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
        
        return healthInsurance;
    }
}