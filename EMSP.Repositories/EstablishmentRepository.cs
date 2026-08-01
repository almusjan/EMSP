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
        return await _dbContext.Establishments.ToListAsync();
    }

    public async Task<Establishment?> GetByIdAsync(Guid? establishmentId)
    {
        return await _dbContext.Establishments
            .Include(e => e.Companies)
            .Include(e => e.Employees)
            .Include(e => e.HealthInsurances)
            .FirstOrDefaultAsync(e => e.Id == establishmentId);
    }

    public async Task<Establishment> UpdateAsync(Establishment establishment)
    {
        _dbContext.Establishments.Update(establishment);
        _dbContext.Entry(establishment).Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
        
        return establishment;
    }
}