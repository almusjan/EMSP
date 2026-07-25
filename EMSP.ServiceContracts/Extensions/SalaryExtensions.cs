using EMSP.Entities.Models;
using EMSP.ServiceContracts.DTOs.SalaryDTOs;

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
            TransportationAllowance = salaryAddRequest.TransportationAllowance,
            TotalSalary =  salaryAddRequest.BasicSalary + (salaryAddRequest.HousingAllowance ?? 0) + (salaryAddRequest.TransportationAllowance ?? 0) + (salaryAddRequest.OtherAllowance ?? 0),
            EmployeeId =  salaryAddRequest.EmployeeId
        };
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
            TotalSalary = salary.TotalSalary,
            EmployeeId = salary.EmployeeId
        };
    }
}