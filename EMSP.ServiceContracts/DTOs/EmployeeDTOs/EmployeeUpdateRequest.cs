using EMSP.ServiceContracts.Enums;

namespace EMSP.ServiceContracts.DTOs.EmployeeDTOs;

public class EmployeeUpdateRequest
{
    public Guid Id { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // personal info
    public string? FullNameAr { get; set; }
    public string? FullNameEn { get; set; }
    public string? IqamaOrIdNumber {get; set;}
    public string? BorderNumber {get; set;} // Non-Saudi
    public DateTime? IqamaOrIdExpiryDate {get; set;}
    public DateTime? DateOfBirth {get; set;}
    public string? Gender {get; set;}
    public string? EmailAddress {get; set;}
    public string? PhoneNumber {get; set;}
    public Guid? CountryId {get; set;}
    public DateTime? PassportExpiryDate {get; set;} // Non-Saudi
    public string? PassportNumber {get; set;} // Non-Saudi
    
    // employment info
    public string? Profession {get; set;}
    public string? ContractNumber {get; set;}
    public DateTime? HireDate {get; set;}
    public EmployeeStatus EmployeeStatus { get; set; }
    public DateTime? TerminationDate {get; set;}
    public Guid? EstablishmentId {get; set;}
    public Guid? CompanyId {get; set;}
    
    // bank info
    public string? Iban {get; set;}
    public string? AccountNumber {get; set;}

    public Guid? BankId { set; get; }
    // only if the employee's bank is not listed
    public string? UnlistedBankName {get; set;} // Non-Saudi
    
    // health care info
    public string? MemberPolicyNumber {get; set;}
    public Guid? HealthInsuranceId {get; set;}
}