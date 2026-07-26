using EMSP.ServiceContracts.DTOs.EmployeeDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeDetailedResponse>> GetEmployees();
    
    Task<EmployeeDetailedResponse> AddEmployee(EmployeeAddRequest? employeeAddRequest);
    
    Task<EmployeeDetailedResponse?> GetEmployeeById(Guid? employeeId);
    
    Task<EmployeeDetailedResponse> UpdateEmployee(EmployeeUpdateRequest? employeeUpdateRequest);
    
    // bool DeleteEmployee(Guid? employeeId);
}