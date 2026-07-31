using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface ISalaryRepository
{
    Task<Salary> AddAsync(Salary salary);
    
    Task<Salary> UpdateAsync(Salary salary);
}