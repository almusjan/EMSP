using System.Linq.Expressions;
using EMSP.Entities;
using EMSP.Entities.Enums;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSP.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext  _dbContext;
    
    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<Employee> GetBaseQuery()
    {
        return _dbContext.Employees
            .Include(e => e.Country)
            .Include(e => e.Establishment)
            .Include(e => e.Company)
            .Include(e => e.HealthInsurance)
            .Include(e => e.Salary)
            .Include(e => e.Bank)
            .Include(e => e.EmployeeCosts);
    }
    
    public async Task<List<Employee>> GetAllAsync() =>
        await GetBaseQuery().ToListAsync();

    public async Task<List<Employee>> GetFilteredAsync(Expression<Func<Employee, bool>> predicate) =>
        await GetBaseQuery().Where(predicate).ToListAsync();

    public async Task<Employee?> GetByIdAsync(Guid? employeeId) =>
        await GetBaseQuery().FirstOrDefaultAsync(e => e.Id == employeeId);
    
    
    public async Task<bool> IsIqamaExistsAsync(string iqamaOrIdNumber) =>
        await _dbContext.Employees.AnyAsync(e => e.IqamaOrIdNumber == iqamaOrIdNumber);

    public async Task<Employee> AddAsync(Employee employee)
    {
        employee.CreatedAt = DateTime.UtcNow;
        employee.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.Employees.AddAsync(employee);
        await  _dbContext.SaveChangesAsync();
        
        return employee;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        Employee? matchingEmployee = await GetBaseQuery().FirstOrDefaultAsync(e => e.Id == employee.Id);

        if (matchingEmployee == null)
            return employee;

        #region CheckingUpdateFields
        
        matchingEmployee.UpdatedAt = DateTime.UtcNow;
        
        matchingEmployee.FullNameAr = employee.FullNameAr;
        matchingEmployee.FullNameEn = employee.FullNameEn;
        matchingEmployee.IqamaOrIdNumber = employee.IqamaOrIdNumber;
        matchingEmployee.IqamaOrIdExpiryDate = employee.IqamaOrIdExpiryDate;
        matchingEmployee.DateOfBirth = employee.DateOfBirth;
        matchingEmployee.Gender =  employee.Gender;
        matchingEmployee.EmailAddress = employee.EmailAddress;
        matchingEmployee.PhoneNumber =  employee.PhoneNumber;
        matchingEmployee.CountryId = employee.CountryId; // nearly impossible Updated 0.0001%
        matchingEmployee.PassportNumber = employee.PassportNumber;
        matchingEmployee.PassportExpiryDate =  employee.PassportExpiryDate;
        
        if(!string.IsNullOrEmpty(employee.BorderNumber))
            matchingEmployee.BorderNumber = employee.BorderNumber;
        
        matchingEmployee.Profession =   employee.Profession;
        matchingEmployee.ContractNumber = employee.ContractNumber;
        matchingEmployee.HireDate = employee.HireDate;
        matchingEmployee.Status = employee.Status;
        matchingEmployee.EstablishmentId =  employee.EstablishmentId;
        matchingEmployee.CompanyId = employee.CompanyId;
        
        if (employee.Status == EmployeeStatus.Terminated)
            matchingEmployee.TerminationDate = employee.TerminationDate;

        matchingEmployee.Iban = employee.Iban;
        matchingEmployee.BankId = employee.BankId;
        matchingEmployee.UnlistedBankName = employee.UnlistedBankName;
        if(!string.IsNullOrEmpty(employee.AccountNumber))
            matchingEmployee.AccountNumber = employee.AccountNumber;
        
        matchingEmployee.HealthInsuranceId = employee.HealthInsuranceId;
        if(employee.HealthInsuranceId != Guid.Empty)
            matchingEmployee.MemberPolicyNumber = employee.MemberPolicyNumber;

        #endregion
        
        await _dbContext.SaveChangesAsync();

        return matchingEmployee;
    }
}