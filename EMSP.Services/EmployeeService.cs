using System.Linq.Expressions;
using EMSP.Entities.Enums;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.EmployeeDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services.Helpers;

namespace EMSP.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    public async Task<List<EmployeeSummaryResponse>> GetEmployees(EmployeeStatus? status = null)
    {
        Expression<Func<Employee, bool>> predicate =
            status.HasValue ? e => !e.IsDeleted && e.Status == status : e => !e.IsDeleted;
        
        List<Employee> employees = await _employeeRepository.GetAllAsync(predicate);
        
        return employees.Select(e => e.ToEmployeeSummaryResponseObject()).ToList();
    }

    public async Task<List<EmployeeSummaryResponse>> GetFilteredEmployees(string filterBy, string searchString)
    {
        List<Employee> employees = (filterBy) switch
        {
            (nameof(Employee.IqamaOrIdNumber)) => await _employeeRepository.GetAllAsync(e => !e.IsDeleted && e.IqamaOrIdNumber != null && e.IqamaOrIdNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ,
            (nameof(Employee.EmailAddress)) => await _employeeRepository.GetAllAsync(e => !e.IsDeleted && e.EmailAddress != null && e.EmailAddress.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ,
            _ => await _employeeRepository.GetAllAsync(e => !e.IsDeleted)
        };
        
        return employees.Select(e => e.ToEmployeeSummaryResponseObject()).ToList();
    }

    public async Task<EmployeeSummaryResponse> AddEmployee(EmployeeAddRequest? employeeAddRequest)
    {
        if(employeeAddRequest == null)
            throw new ArgumentNullException(nameof(employeeAddRequest));
        
        ValidationHelper.ModelValidation(employeeAddRequest);

        if (employeeAddRequest.IqamaOrIdNumber != null && await _employeeRepository.IsIqamaExistsAsync(employeeAddRequest.IqamaOrIdNumber))
            throw new InvalidOperationException($"The employee with Iqama {employeeAddRequest.IqamaOrIdNumber} already exists");
        
        Employee employee = employeeAddRequest.ToEmployeeObject();

        employee.Id = Guid.NewGuid();

        await _employeeRepository.AddAsync(employee);

        return employee.ToEmployeeSummaryResponseObject();
    }

    public async Task<EmployeeDetailedResponse?> GetEmployeeById(Guid? employeeId)
    {
        if (employeeId == null)
            throw new ArgumentNullException(nameof(employeeId));
        
        Employee? employee = await _employeeRepository.GetByIdAsync(employeeId.Value);

        // hide soft deleted (for now at least)
        if (employee == null || employee.IsDeleted)
            throw new KeyNotFoundException($"The employee with ID {employeeId} not found or soft-deleted");
        
        return employee.ToEmployeeDetailedResponseObject();
    }

    public async Task<EmployeeSummaryResponse> UpdateEmployee(EmployeeUpdateRequest? employeeUpdateRequest)
    {
        if (employeeUpdateRequest == null)
            throw new ArgumentNullException(nameof(employeeUpdateRequest));
        
        ValidationHelper.ModelValidation(employeeUpdateRequest);
        
        Employee? matchingEmployee = await _employeeRepository.GetByIdAsync(employeeUpdateRequest.Id);
        
        if(matchingEmployee == null)
            throw new KeyNotFoundException($"The employee with ID {employeeUpdateRequest.Id} not found");

        #region CheckingUpdateFields
        
        matchingEmployee.FullNameAr = employeeUpdateRequest.FullNameAr;
        matchingEmployee.FullNameEn = employeeUpdateRequest.FullNameEn;
        matchingEmployee.IqamaOrIdNumber = employeeUpdateRequest.IqamaOrIdNumber;
        matchingEmployee.IqamaOrIdExpiryDate = employeeUpdateRequest.IqamaOrIdExpiryDate;
        matchingEmployee.DateOfBirth = employeeUpdateRequest.DateOfBirth;
        matchingEmployee.Gender =  employeeUpdateRequest.Gender;
        matchingEmployee.EmailAddress = employeeUpdateRequest.EmailAddress;
        matchingEmployee.PhoneNumber =  employeeUpdateRequest.PhoneNumber;
        matchingEmployee.CountryId = employeeUpdateRequest.CountryId; // nearly impossible Updated 0.0001%
        matchingEmployee.PassportNumber = employeeUpdateRequest.PassportNumber;
        matchingEmployee.PassportExpiryDate =  employeeUpdateRequest.PassportExpiryDate;
        
        if(!string.IsNullOrEmpty(employeeUpdateRequest.BorderNumber))
            matchingEmployee.BorderNumber = employeeUpdateRequest.BorderNumber;
        
        matchingEmployee.Profession =   employeeUpdateRequest.Profession;
        matchingEmployee.ContractNumber = employeeUpdateRequest.ContractNumber;
        matchingEmployee.HireDate = employeeUpdateRequest.HireDate;
        matchingEmployee.Status = employeeUpdateRequest.Status;
        matchingEmployee.EstablishmentId =  employeeUpdateRequest.EstablishmentId;
        matchingEmployee.CompanyId = employeeUpdateRequest.CompanyId;
        
        if (employeeUpdateRequest.Status == EmployeeStatus.Terminated)
            matchingEmployee.TerminationDate = employeeUpdateRequest.TerminationDate;

        matchingEmployee.Iban = employeeUpdateRequest.Iban;
        matchingEmployee.BankId = employeeUpdateRequest.BankId;
        matchingEmployee.UnlistedBankName = employeeUpdateRequest.UnlistedBankName;
        if(!string.IsNullOrEmpty(employeeUpdateRequest.AccountNumber))
            matchingEmployee.AccountNumber = employeeUpdateRequest.AccountNumber;
        
        matchingEmployee.HealthInsuranceId = employeeUpdateRequest.HealthInsuranceId;
        if(employeeUpdateRequest.HealthInsuranceId != Guid.Empty)
            matchingEmployee.MemberPolicyNumber = employeeUpdateRequest.MemberPolicyNumber;

        #endregion

        await _employeeRepository.UpdateAsync(matchingEmployee);
        
        return  matchingEmployee.ToEmployeeSummaryResponseObject();
    }

    public async Task SoftDeleteEmployee(Guid employeeId)
    {
        Employee? matchingEmployee = await _employeeRepository.GetByIdAsync(employeeId);

        // not sure
        if (matchingEmployee == null)
            throw new KeyNotFoundException($"The employee with ID {employeeId} not found");

        if (matchingEmployee.IsDeleted)
            throw new InvalidOperationException("The employee is already soft-deleted");
        
        if (matchingEmployee.Status == EmployeeStatus.Active)
            throw new InvalidOperationException("Cannot soft-delete an active employee. Terminate him first.");

        matchingEmployee.IsDeleted = true;

        await _employeeRepository.UpdateAsync(matchingEmployee);
    }
    
    // I may add 2 more methods - get soft deleted list - hard delete for employee who was terminated for at least 3 months
}