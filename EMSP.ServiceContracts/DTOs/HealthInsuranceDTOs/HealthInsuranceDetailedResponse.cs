using EMSP.ServiceContracts.DTOs.EmployeeDTOs;
using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

namespace EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;

public class HealthInsuranceDetailedResponse
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime? UpdatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    
    public string? PolicyNumber {get; set;}
    public string? InsuranceProvider {get; set;}
    public DateTime? PolicyExpiryDate {get; set;}
    
    // est. dto
    public EstablishmentSummaryResponse? Establishment {get; set;}
    
    // list of employees dto
    public List<EmployeeSummaryResponse>? Employees {get; set;}
}