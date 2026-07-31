using EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;
using EMSP.ServiceContracts.Interfaces;

namespace EMSP.Services;

public class EmployeeCostService : IEmployeeCostService
{
    public async Task<EmployeeCostResponse> AddEmployeeCost(EmployeeCostAddRequest? employeeCostAddRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<EmployeeCostResponse?> GetEmployeeCostById(Guid? employeeCostId)
    {
        throw new NotImplementedException();
    }

    public async Task<EmployeeCostResponse> UpdateEmployeeCost(EmployeeCostUpdateRequest? employeeCostUpdateRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SoftDeleteEmployeeCost(Guid? employeeCostId)
    {
        throw new NotImplementedException();
    }
}