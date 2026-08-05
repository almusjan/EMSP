using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface ISalaryRepository
{
    Task<Salary> AddAsync(Guid? employeeId, Salary salary);
    
    Task<Salary?> GetByIdAsync(Guid? salaryId);
    
    Task<Salary> UpdateAsync(Salary salary);
}