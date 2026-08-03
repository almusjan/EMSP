using System.Linq.Expressions;
using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface IEstablishmentRepository
{
    Task<Establishment> AddAsync(Establishment establishment);
    
    Task<List<Establishment>> GetAllAsync(Expression<Func<Establishment, bool>> predicate);
    
    Task<Establishment?> GetByIdAsync(Guid? establishmentId);
    
    Task<Establishment> UpdateAsync(Establishment establishment);
}