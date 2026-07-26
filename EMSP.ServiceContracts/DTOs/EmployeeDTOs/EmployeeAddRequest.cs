using EMSP.Entities.Enums;

namespace EMSP.ServiceContracts.DTOs.EmployeeDTOs;

public class EmployeeAddRequest
{
    // Full Name | Iqama/ID No/ExpireDate | DOB | Gender | Email | Phone | Country ID
    // Profession | Hire Date | Establishment/Company ID 
    
    // Personal Info
    public string? FullNameAr { get; set; }
    public string? FullNameEn { get; set; }
    public string? BorderNumber {get; set;} // Non-Saudi
    public string? IqamaOrIdNumber {get; set;}
    public DateTime? IqamaOrIdExpiryDate {get; set;}
    public DateTime? DateOfBirth { get; set; }
    public GenderOptions? Gender { get; set; }
    public string? EmailAddress {get; set;}
    public string? PhoneNumber { get; set; }
    public Guid? CountryId {get; set;}
    
    // Employment Info
    public string? Profession {get; set;}
    public DateTime? HireDate {get; set;}
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
    public Guid? EstablishmentId {get; set;}
    public Guid? CompanyId {get; set;}
}