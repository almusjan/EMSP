using EMSP.Entities.Models;
using EMSP.ServiceContracts.DTOs.SalaryDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface ISalaryService
{
    
    Task<SalaryResponse> AddSalary(Guid? employeeId, SalaryAddRequest? salaryAddRequest);
    
     Task<SalaryResponse> UpdateSalary(Guid? employeeId, SalaryUpdateRequest? salaryUpdateRequest);
    
}