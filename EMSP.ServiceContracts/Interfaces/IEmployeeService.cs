using EMSP.Entities.Enums;
using EMSP.ServiceContracts.DTOs.EmployeeDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeSummaryResponse>> GetEmployees(EmployeeStatus? status = null);

    Task<List<EmployeeSummaryResponse>> GetFilteredEmployees(string filterBy, string searchString); 
    
    Task<EmployeeSummaryResponse> AddEmployee(EmployeeAddRequest? employeeAddRequest);
    
    Task<EmployeeDetailedResponse?> GetEmployeeById(Guid? employeeId);
    
    Task<EmployeeSummaryResponse> UpdateEmployee(EmployeeUpdateRequest? employeeUpdateRequest);
    
    Task<bool> SoftDeleteEmployee(Guid? employeeId);
}