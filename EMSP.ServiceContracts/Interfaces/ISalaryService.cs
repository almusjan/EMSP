using EMSP.Entities.Models;
using EMSP.ServiceContracts.DTOs.SalaryDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface ISalaryService
{
    
    Task<SalaryResponse> AddSalary(SalaryAddRequest? salaryAddRequest);
    
     Task<SalaryResponse> UpdateSalary(SalaryUpdateRequest? salaryUpdateRequest);
    
}