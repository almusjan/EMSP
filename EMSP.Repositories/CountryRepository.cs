using EMSP.Entities;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSP.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CountryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Country>> GetAllAsync()
    {
        return await _dbContext.Countries.ToListAsync();
    }

    public async Task<Country> AddAsync(Country country)
    {
        country.CreatedAt = DateTime.UtcNow;
        country.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.Countries.AddAsync(country);
        await _dbContext.SaveChangesAsync();
        
        return country;
    }
}