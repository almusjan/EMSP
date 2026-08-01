using EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IEmployeeCostService
{
    Task<EmployeeCostResponse> AddEmployeeCost(EmployeeCostAddRequest? employeeCostAddRequest);

    Task<EmployeeCostResponse?> GetEmployeeCostById(Guid? employeeCostId);
    
    Task<EmployeeCostResponse> UpdateEmployeeCost(EmployeeCostUpdateRequest? employeeCostUpdateRequest);
    
    Task SoftDeleteEmployeeCost(Guid employeeCostId);
}