namespace EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;

public class HealthInsuranceAddRequest
{
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string? PolicyNumber {get; set;}
    public string? InsuranceProvider {get; set;}
    public DateTime? PolicyExpiryDate {get; set;}
    public Guid? EstablishmentId {get; set;}
}