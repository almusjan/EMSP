using EMSP.Entities;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSP.Repositories;

public class BankRepository : IBankRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BankRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Bank>> GetAllAsync()
    {
        return await _dbContext.Banks.ToListAsync();
    }

    public async Task<Bank> AddAsync(Bank bank)
    {
        bank.CreatedAt = DateTime.UtcNow;
        bank.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.Banks.AddAsync(bank);
        await _dbContext.SaveChangesAsync();
        
        return bank;
    }
}