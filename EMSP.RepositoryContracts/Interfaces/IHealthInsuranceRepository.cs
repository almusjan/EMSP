using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface IHealthInsuranceRepository
{
    Task<HealthInsurance> AddAsync(HealthInsurance healthInsurance);
    
    Task<List<HealthInsurance>> GetAllAsync();
    
    Task<HealthInsurance?> GetByIdAsync(Guid? healthInsuranceId);
    
    Task<HealthInsurance> UpdateAsync(HealthInsurance healthInsurance);
}