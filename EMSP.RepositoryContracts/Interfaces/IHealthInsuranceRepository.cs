using System.Linq.Expressions;
using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface IHealthInsuranceRepository
{
    Task<HealthInsurance> AddAsync(HealthInsurance healthInsurance);
    
    Task<List<HealthInsurance>> GetAllAsync(Expression<Func<HealthInsurance, bool>> predicate);
    
    Task<HealthInsurance?> GetByIdAsync(Guid? healthInsuranceId);
    
    Task<HealthInsurance> UpdateAsync(HealthInsurance healthInsurance);
}