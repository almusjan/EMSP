using EMSP.Entities.Models;
using EMSP.ServiceContracts.DTOs.SalaryDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface ISalaryService
{
    Task<List<SalaryResponse>> GetSalaries();
    
    Task<SalaryResponse> AddSalary(SalaryAddRequest? salaryAddRequest);
    
     Task<SalaryResponse?> GetSalary(Guid?  salaryId);
    
     Task<SalaryResponse> UpdateSalary(SalaryUpdateRequest? salaryUpdateRequest);
    
    // bool DeleteSalary(Guid? salaryId);
}