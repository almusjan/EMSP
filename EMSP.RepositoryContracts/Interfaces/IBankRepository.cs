using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface IBankRepository
{
    Task<List<Bank>>  GetAllAsync();
    
    Task<Bank>  AddAsync(Bank bank);
}