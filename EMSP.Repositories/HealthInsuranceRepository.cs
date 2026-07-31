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

    private IQueryable<HealthInsurance> GetBaseQuery()
    {
        return _dbContext.HealthInsurances.Include(hi => hi.Establishment)
            .Include(hi => hi.Employees);
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
        return await GetBaseQuery().ToListAsync();
    }

    public async Task<HealthInsurance?> GetByIdAsync(Guid? healthInsuranceId)
    {
        return await GetBaseQuery().FirstOrDefaultAsync(hi => hi.Id == healthInsuranceId);
    }

    public async Task<HealthInsurance> UpdateAsync(HealthInsurance healthInsurance)
    {
        HealthInsurance? matchingHealthInsurance =
            await GetBaseQuery().FirstOrDefaultAsync(hi => hi.Id == healthInsurance.Id);

        if (matchingHealthInsurance == null)
            return healthInsurance;

        #region CheckingUpdateFields

        matchingHealthInsurance.UpdatedAt = DateTime.UtcNow;
        
        matchingHealthInsurance.PolicyExpiryDate = healthInsurance.PolicyExpiryDate;
        matchingHealthInsurance.PolicyNumber = healthInsurance.PolicyNumber;
        matchingHealthInsurance.InsuranceProvider = healthInsurance.InsuranceProvider;
        matchingHealthInsurance.EstablishmentId = healthInsurance.EstablishmentId;

        #endregion
        
        await _dbContext.SaveChangesAsync();
        
        return matchingHealthInsurance;
    }
}