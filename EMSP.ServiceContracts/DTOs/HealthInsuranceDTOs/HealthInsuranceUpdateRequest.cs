namespace EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;

public class HealthInsuranceUpdateRequest
{
    public Guid Id { get; set; }
    
    public string? PolicyNumber {get; set;}
    public string? InsuranceProvider {get; set;}
    public DateTime? PolicyExpiryDate {get; set;}
    public Guid? EstablishmentId {get; set;}
}