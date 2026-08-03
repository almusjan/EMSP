using EMSP.ServiceContracts.DTOs.SalaryDTOs;
using EMSP.Entities.Models;

namespace EMSP.ServiceContracts.Extensions;

public static class SalaryExtensions
{
    // convert add request to salary object
    public static Salary ToSalaryObject(this SalaryAddRequest salaryAddRequest)
    {
        return new Salary()
        {
            BasicSalary = salaryAddRequest.BasicSalary,
            HousingAllowance =  salaryAddRequest.HousingAllowance,
            OtherAllowance = salaryAddRequest.OtherAllowance,
            TransportationAllowance = salaryAddRequest.TransportationAllowance
        };
    }
    // Calculate before saving
    public static void CalculateTotalSalary (this Salary salary)
    {
        salary.TotalSalary = salary.BasicSalary + (salary.HousingAllowance ?? 0) +
                             (salary.TransportationAllowance ?? 0) + (salary.OtherAllowance ?? 0);
    }
    
    // convert salary to response object
    public static SalaryResponse ToSalaryResponseObject(this Salary salary)
    {
        return new SalaryResponse()
        {
            Id =  salary.Id,
            UpdatedAt =  salary.UpdatedAt,
            CreatedAt =   salary.CreatedAt,
            UpdatedBy =   salary.UpdatedBy,
            CreatedBy =  salary.CreatedBy,
            
            BasicSalary = salary.BasicSalary,
            HousingAllowance = salary.HousingAllowance,
            OtherAllowance = salary.OtherAllowance,
            TransportationAllowance = salary.TransportationAllowance,
            TotalSalary = salary.TotalSalary
        };
    }
    
    // only for unit test
    public static SalaryUpdateRequest ToSalaryUpdateRequest(this SalaryResponse salaryResponse)
    {
        return new SalaryUpdateRequest()
        {
            Id =  salaryResponse.Id,
            BasicSalary = salaryResponse.BasicSalary,
            HousingAllowance = salaryResponse.HousingAllowance,
            OtherAllowance = salaryResponse.OtherAllowance,
            TransportationAllowance = salaryResponse.TransportationAllowance
        };
    }
}