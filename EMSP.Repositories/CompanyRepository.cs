using EMSP.Entities;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSP.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CompanyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Company>> GetAllAsync() => 
        await _dbContext.Companies.ToListAsync();

    public async Task<Company?> GetByIdAsync(Guid companyId)
    {
        return await _dbContext.Companies
            .Include(c => c.Employees)
            .Include(c => c.Establishment)
            .FirstOrDefaultAsync(e => e.Id == companyId);
    }

    public async Task<Company> AddAsync(Company company)
    {
        company.CreatedAt =  DateTime.UtcNow;
        company.UpdatedAt = DateTime.UtcNow;
        
        await  _dbContext.Companies.AddAsync(company);
        await _dbContext.SaveChangesAsync();
        
        return company;
    }

    public async Task<Company> UpdateAsync(Company company)
    {
        _dbContext.Companies.Update(company);
        _dbContext.Entry(company).Property(c => c.UpdatedAt).CurrentValue = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
        
        return company;
    }
}