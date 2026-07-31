using EMSP.Entities;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSP.Repositories;

public class EstablishmentRepository : IEstablishmentRepository
{
    private readonly ApplicationDbContext _dbContext;
    public EstablishmentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<Establishment> GetBaseQuery()
    {
        return _dbContext.Establishments.Include(e => e.Companies)
            .Include(e => e.Employees)
            .Include(e => e.HealthInsurances);
    }
    
    public async Task<Establishment> AddAsync(Establishment establishment)
    {
        establishment.CreatedAt =  DateTime.UtcNow;
        establishment.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.Establishments.AddAsync(establishment);
        await _dbContext.SaveChangesAsync();
        
        return establishment;
    }

    public async Task<List<Establishment>> GetAllAsync()
    {
        return await GetBaseQuery().ToListAsync();
    }

    public async Task<Establishment?> GetByIdAsync(Guid? establishmentId)
    {
        return await GetBaseQuery().FirstOrDefaultAsync(e => e.Id == establishmentId);
    }

    public async Task<Establishment> UpdateAsync(Establishment establishment)
    {
        Establishment? matchingEstablishment = await GetBaseQuery().FirstOrDefaultAsync(e => e.Id == establishment.Id);

        if (matchingEstablishment == null)
            return establishment;

        #region CheckingUpdateFields
        
        matchingEstablishment.UpdatedAt = DateTime.UtcNow;

        matchingEstablishment.EstablishmentNameAr =  establishment.EstablishmentNameAr;
        matchingEstablishment.EstablishmentNameEn = establishment.EstablishmentNameEn;
        matchingEstablishment.EstablishmentCode =  establishment.EstablishmentCode;
        matchingEstablishment.EstablishmentType = establishment.EstablishmentType;
        matchingEstablishment.NationalId =  establishment.NationalId;
        matchingEstablishment.CommercialRegistrationNumber =  establishment.CommercialRegistrationNumber;
        matchingEstablishment.ShortAddress =  establishment.ShortAddress;
        matchingEstablishment.FullAddress  =  establishment.FullAddress;
        matchingEstablishment.VatNumber =   establishment.VatNumber;

        #endregion
        
        await _dbContext.SaveChangesAsync();
        
        return matchingEstablishment;
    }
}