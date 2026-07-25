using EMSP.ServiceContracts.DTOs.EmployeeDTOs;
using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

namespace EMSP.ServiceContracts.DTOs.CompanyDTOs;

public class CompanyDetailedResponse
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    
    public string? CompanyNameAr {get; set;}
    public string? CompanyNameEn {get; set;}
    public string? CompanyCode {get; set;}
    public string? ShortAddress {get; set;}
    public string? FullAddress {get; set;}
    public string? ContactNumber {get; set;}
    public string? Email {get; set;}
    public string? VatNumber { get;set; }
    
    // est. dto
    public EstablishmentSummaryResponse? Establishment {get; set;}
    
    // list of employees dto
    public List<EmployeeSummaryResponse>? Employees {get; set;}
}