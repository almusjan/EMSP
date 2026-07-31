using EMSP.Entities.Enums;
using EMSP.ServiceContracts.DTOs.BankDTOs;
using EMSP.ServiceContracts.DTOs.CompanyDTOs;
using EMSP.ServiceContracts.DTOs.CountryDTOs;
using EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;
using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;
using EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;
using EMSP.ServiceContracts.DTOs.SalaryDTOs;

namespace EMSP.ServiceContracts.DTOs.EmployeeDTOs;

public class EmployeeDetailedResponse
{
    // all fields - replace Ids by DTOs later
    
    // Base Info
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime? UpdatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    
    // Personal Info
    public string? FullNameAr { get; set; }
    public string? FullNameEn { get; set; }
    public string? IqamaOrIdNumber {get; set;}
    public string? PassportNumber {get; set;}
    public string? BorderNumber {get; set;}
    public DateTime? IqamaOrIdExpiryDate {get; set;}
    public DateTime? PassportExpiryDate {get; set;}
    public DateTime? DateOfBirth { get; set; }
    public GenderOptions? Gender { get; set; }
    public string? EmailAddress {get; set;}
    public string? PhoneNumber { get; set; }
    
    // country summary DTO
    public CountryResponse? Country {get; set;}
    
    // Employment Info
    public string? Profession {get; set;}
    public string? ContractNumber {get; set;}
    public DateTime? HireDate {get; set;}
    public EmployeeStatus? Status { get; set; }
    public DateTime? TerminationDate {get; set;}
    // est. & company summary DTOs
    public EstablishmentSummaryResponse?  Establishment {get; set;}
    public CompanySummaryResponse?  Company {get; set;}
    
    // Bank Info
    public string? Iban {get; set;}
    public string? AccountNumber {get; set;}
    public string? UnlistedBankName { get; set; }
    // bank summary DTO
    public BankResponse? Bank {get; set;}
    
    // Salary Info
    // salary summary DTO
    public SalaryResponse?  Salary {get; set;}
    
    // Health Care Info
    public string? MemberPolicyNumber {get; set;}
    // health insurance summary DTO
    public HealthInsuranceSummaryResponse?  HealthInsurance {get; set;}
    
    // Employee Costs
    public List<EmployeeCostResponse>? EmployeeCosts {get; set;}
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        
        if(obj.GetType() != typeof(EmployeeDetailedResponse))
            return false;
        
        EmployeeDetailedResponse otherResponse = (EmployeeDetailedResponse)obj;
        
        return Id ==  otherResponse.Id && IqamaOrIdNumber == otherResponse.IqamaOrIdNumber;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"{IqamaOrIdNumber} - {FullNameEn}|{FullNameAr}";
    }
}