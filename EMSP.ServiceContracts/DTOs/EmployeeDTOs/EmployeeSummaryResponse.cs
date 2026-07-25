using EMSP.Entities.Models;

namespace EMSP.ServiceContracts.DTOs.EmployeeDTOs;

public class EmployeeSummaryResponse
{
    public Guid Id {get; set;}
    public string? FullNameAr { get; set; }
    public string? FullNameEn { get; set; }
    public string? IqamaOrIdNumber {get; set;}
    public DateTime? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CountryCode { get; set; }
    public string? Profession {get; set;}
    public DateTime? HireDate {get; set;}
    public string? EstablishmentCode { get; set; }
    public string? CompanyCode { get; set; }
    public bool? HasMemberPolicyNumber {get; set;}
    public bool? IsTerminated {get; set;}
}