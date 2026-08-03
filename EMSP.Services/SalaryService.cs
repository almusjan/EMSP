using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.SalaryDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services.Helpers;

namespace EMSP.Services;

public class SalaryService : ISalaryService
{
    private readonly ISalaryRepository _salaryRepository;

    public SalaryService(ISalaryRepository salaryRepository)
    {
        _salaryRepository = salaryRepository;
    }
    
    public async Task<SalaryResponse> AddSalary(SalaryAddRequest? salaryAddRequest)
    {
        if(salaryAddRequest == null)
            throw new ArgumentNullException(nameof(salaryAddRequest));
        
        ValidationHelper.ModelValidation(salaryAddRequest);
        
        Salary salary = salaryAddRequest.ToSalaryObject();
        salary.Id =  Guid.NewGuid();
        
        await _salaryRepository.AddAsync(salary);

        return salary.ToSalaryResponseObject();
    }

    public async Task<SalaryResponse> UpdateSalary(SalaryUpdateRequest? salaryUpdateRequest)
    {
        if (salaryUpdateRequest == null)
            throw new ArgumentNullException(nameof(salaryUpdateRequest));
        
        ValidationHelper.ModelValidation(salaryUpdateRequest);

        Salary? matchingSalary = await _salaryRepository.GetByIdAsync(salaryUpdateRequest.Id);

        if (matchingSalary == null)
            throw new KeyNotFoundException("The employee's salary not found");

        #region CheckingUpdateFields
        
        matchingSalary.BasicSalary =  salaryUpdateRequest.BasicSalary;
        matchingSalary.HousingAllowance = salaryUpdateRequest.HousingAllowance;
        matchingSalary.OtherAllowance = salaryUpdateRequest.OtherAllowance;
        matchingSalary.TransportationAllowance =  salaryUpdateRequest.TransportationAllowance;
        matchingSalary.CalculateTotalSalary();
        
        #endregion
        
        await _salaryRepository.UpdateAsync(matchingSalary);
        
        return  matchingSalary.ToSalaryResponseObject();
    }
}