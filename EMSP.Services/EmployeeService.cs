using EMSP.ServiceContracts.DTOs.EmployeeDTOs;
using EMSP.ServiceContracts.Interfaces;

namespace EMSP.Services;

public class EmployeeService : IEmployeeService
{
    public async Task<List<EmployeeDetailedResponse>> GetEmployees()
    {
        throw new NotImplementedException();
    }

    public async Task<EmployeeDetailedResponse> AddEmployee(EmployeeAddRequest? employeeAddRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<EmployeeDetailedResponse?> GetEmployeeById(Guid? employeeId)
    {
        throw new NotImplementedException();
    }

    public async Task<EmployeeDetailedResponse> UpdateEmployee(EmployeeUpdateRequest? employeeUpdateRequest)
    {
        throw new NotImplementedException();
    }
}