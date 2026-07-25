using EMSP.Entities.Models;
using EMSP.ServiceContracts.DTOs;
using EMSP.ServiceContracts.DTOs.EmployeeDTOs;

namespace EMSP.ServiceContracts.Extensions;

public static class EmployeeExtensions
{
    // Convert AddRequest to Employee Object
    public static Employee ToEmployeeObject(this EmployeeAddRequest employeeAddRequest)
    {
        return new Employee()
        {
            FullNameAr = employeeAddRequest.FullNameAr,
            FullNameEn = employeeAddRequest.FullNameEn,
            IqamaOrIdNumber = employeeAddRequest.IqamaOrIdNumber,
            IqamaOrIdExpiryDate = employeeAddRequest.IqamaOrIdExpiryDate,
            DateOfBirth = employeeAddRequest.DateOfBirth,
            Gender = employeeAddRequest.Gender?.ToString(),
            EmailAddress = employeeAddRequest.EmailAddress,
            PhoneNumber = employeeAddRequest.PhoneNumber,
            CountryId = employeeAddRequest.CountryId,
            Profession = employeeAddRequest.Profession,
            HireDate = employeeAddRequest.HireDate,
            EstablishmentId = employeeAddRequest.EstablishmentId,
            CompanyId = employeeAddRequest.CompanyId,
            EmployeeStatus = employeeAddRequest.EmployeeStatus.ToString(),
            CreatedBy = employeeAddRequest.CreatedBy
        };
    }
    
    // Convert Employee to Response Object
    public static EmployeeDetailedResponse ToEmployeeDetailedResponseObject(this Employee employee)
    {
        return new EmployeeDetailedResponse()
        {
            Id = employee.Id,
            CreatedBy = employee.CreatedBy,
            CreatedAt = employee.CreatedAt,
            UpdatedBy = employee.UpdatedBy,
            UpdatedAt = employee.UpdatedAt,
            
            FullNameAr = employee.FullNameAr,
            FullNameEn = employee.FullNameEn,
            IqamaOrIdNumber = employee.IqamaOrIdNumber,
            PassportNumber = employee.PassportNumber,
            BorderNumber = employee.BorderNumber,
            IqamaOrIdExpiryDate = employee.IqamaOrIdExpiryDate,
            PassportExpiryDate = employee.PassportExpiryDate,
            DateOfBirth = employee.DateOfBirth,
            Gender = employee.Gender,
            EmailAddress = employee.EmailAddress,
            PhoneNumber = employee.PhoneNumber,
            
            // country dto
            Country = employee.Country?.ToCountryResponseObject(),
            
            Profession = employee.Profession,
            ContractNumber = employee.ContractNumber,
            HireDate = employee.HireDate,
            EmployeeStatus = employee.EmployeeStatus?.ToString(),
            IsTerminated =  employee.IsTerminated,
            TerminationDate = employee.TerminationDate,
            // est dto
            Establishment = employee.Establishment?.ToEstablishmentSummaryResponseObject(),
            // company dto
            Company = employee.Company?.ToCompanySummaryResponseObject(),
            
            Iban = employee.Iban,
            AccountNumber = employee.AccountNumber,
            UnlistedBankName = employee.UnlistedBankName,
            //bank dto
            Bank = employee.Bank?.ToBankResponseObject(),
            
            // salary dto
            Salary = employee.Salary?.ToSalaryResponseObject(),
            
            MemberPolicyNumber = employee.MemberPolicyNumber,
            //health insurance dto
            HealthInsurance = employee.HealthInsurance?.ToHealthInsuranceSummaryResponseObject(),
            
            // list of employee costs dto
            EmployeeCosts = employee.EmployeeCosts?.Select(ec => ec.ToEmployeeCostResponseObject()).ToList()
        };
    }
    
    // Convert Employee to Summary Response Object
    public static EmployeeSummaryResponse ToEmployeeSummaryResponseObject(this Employee employee)
    {
        return new EmployeeSummaryResponse()
        {
            Id =  employee.Id,
            FullNameAr = employee.FullNameAr,
            FullNameEn = employee.FullNameEn,
            IqamaOrIdNumber = employee.IqamaOrIdNumber,
            DateOfBirth = employee.DateOfBirth,
            PhoneNumber = employee.PhoneNumber,
            CountryCode = employee.Country?.CountryCode,
            Profession = employee.Profession,
            HireDate = employee.HireDate,
            EstablishmentCode = employee.Establishment?.EstablishmentCode,
            CompanyCode = employee.Company?.CompanyCode,
            HasMemberPolicyNumber = !string.IsNullOrEmpty(employee.MemberPolicyNumber),
            IsTerminated = employee.IsTerminated
        };
    }
}