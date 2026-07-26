using EMSP.ServiceContracts.DTOs.SalaryDTOs;
using EMSP.ServiceContracts.Interfaces;

namespace EMSP.Services;

public class SalaryService : ISalaryService
{
    public async Task<List<SalaryResponse>> GetSalaries()
    {
        throw new NotImplementedException();
    }

    public async Task<SalaryResponse> AddSalary(SalaryAddRequest? salaryAddRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<SalaryResponse?> GetSalary(Guid? salaryId)
    {
        throw new NotImplementedException();
    }

    public async Task<SalaryResponse> UpdateSalary(SalaryUpdateRequest? salaryUpdateRequest)
    {
        throw new NotImplementedException();
    }
}