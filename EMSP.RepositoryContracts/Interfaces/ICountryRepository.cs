using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface ICountryRepository
{
    Task<List<Country>> GetAllAsync();
    
    Task<Country> AddAsync(Country country);
}