using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface IEmployeeCostRepository
{
    Task<EmployeeCost?> GetByIdAsync(Guid? employeeCostId);
    
    Task<EmployeeCost> AddAsync(EmployeeCost employeeCost);
    
    Task<EmployeeCost> UpdateAsync(EmployeeCost employeeCost);
}