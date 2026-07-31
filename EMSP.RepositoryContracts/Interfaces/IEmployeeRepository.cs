using System.Linq.Expressions;
using EMSP.Entities.Enums;
using EMSP.Entities.Models;

namespace EMSP.RepositoryContracts.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();
    
    Task<List<Employee>> GetFilteredAsync(Expression<Func<Employee, bool>>  predicate);
    
    Task<Employee?> GetByIdAsync(Guid? employeeId);
    
    Task<Employee>  AddAsync(Employee employee);
    
    Task<Employee> UpdateAsync(Employee employee);

    Task<bool> IsIqamaExistsAsync(string iqamaOrIdNumber);
}