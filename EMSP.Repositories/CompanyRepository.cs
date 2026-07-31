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

    private IQueryable<Company> GetBaseQuery()
    {
        return _dbContext.Companies.Include(e => e.Employees)
            .Include(e => e.Establishment);
    }
    
    public async Task<List<Company>> GetAllAsync() => 
        await GetBaseQuery().ToListAsync();

    public async Task<Company?> GetByIdAsync(Guid companyId) =>
        await GetBaseQuery().FirstOrDefaultAsync(e => e.Id == companyId);

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
        Company? matchingCompany = await GetBaseQuery().FirstOrDefaultAsync(c => c.Id == company.Id);

        if (matchingCompany == null)
            return company;

        #region CheckingUpdateFields

        matchingCompany.UpdatedAt = DateTime.UtcNow;
        
        matchingCompany.CompanyNameAr = company.CompanyNameAr;
        matchingCompany.CompanyNameEn = company.CompanyNameEn;
        matchingCompany.CompanyCode =  company.CompanyCode;
        matchingCompany.ShortAddress = company.ShortAddress;
        matchingCompany.FullAddress =  company.FullAddress;
        matchingCompany.ContactNumber = company.ContactNumber;
        matchingCompany.Email = company.Email;
        matchingCompany.VatNumber = company.VatNumber;
        matchingCompany.EstablishmentId = company.EstablishmentId;

        #endregion
        
        await _dbContext.SaveChangesAsync();
        
        return matchingCompany;
    }
}