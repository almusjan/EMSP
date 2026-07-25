using EMSP.ServiceContracts.DTOs.CompanyDTOs;
using EMSP.ServiceContracts.DTOs.EmployeeDTOs;
using EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;

namespace EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

public class EstablishmentDetailedResponse
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    
    public string? EstablishmentNameAr { get; set; }
    public string? EstablishmentNameEn { get; set; }
    public string? EstablishmentCode { get; set; }
    public string? EstablishmentType {get; set;}
    public string? NationalId {get; set;}
    public string? CommercialRegistrationNumber {get; set;}
    public string? ShortAddress {get; set;}
    public string? FullAddress {get; set;}
    public string? VatNumber { get;set; }
    
    // list of employees dto
    public List<EmployeeSummaryResponse>?  Employees {get; set;}
    // list of companies dto
    public List<CompanySummaryResponse>?  Companies {get; set;}
    // list of health insurances dto
    public List<HealthInsuranceSummaryResponse>?  HealthInsurances {get; set;}
}