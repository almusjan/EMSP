using EMSP.ServiceContracts.DTOs.SalaryDTOs;
using EMSP.ServiceContracts.Interfaces;

namespace EMSP.Services;

public class SalaryService : ISalaryService
{
    public async Task<SalaryResponse> AddSalary(Guid? employeeId, SalaryAddRequest? salaryAddRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<SalaryResponse> UpdateSalary(Guid? employeeId, SalaryUpdateRequest? salaryUpdateRequest)
    {
        throw new NotImplementedException();
    }
}