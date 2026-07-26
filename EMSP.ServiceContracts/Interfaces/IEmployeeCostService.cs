using EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IEmployeeCostService
{
    Task<List<EmployeeCostResponse>> GetEmployeeCosts();
    
    Task<EmployeeCostResponse> AddEmployeeCost(EmployeeCostAddRequest? employeeCostAddRequest);

    Task<EmployeeCostResponse?> GetEmployeeCostById(Guid? employeeCostId);
    
    Task<EmployeeCostResponse> UpdateEmployeeCost(EmployeeCostUpdateRequest? employeeCostUpdateRequest);
    
    //bool DeleteEmployeeCost(Guid? employeeCostId);
}