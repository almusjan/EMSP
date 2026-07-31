using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllAsync();
    
    Task<Company?> GetByIdAsync(Guid companyId);
    
    Task<Company> AddAsync(Company company);
    
    Task<Company> UpdateAsync(Company company);
}