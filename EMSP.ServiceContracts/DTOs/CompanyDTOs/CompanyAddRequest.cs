namespace EMSP.ServiceContracts.DTOs.CompanyDTOs;

public class CompanyAddRequest
{
    public Guid? CreatedBy {get; set;}
    public DateTime CreatedAt {get; set;} =  DateTime.UtcNow;
    
    public string? CompanyNameAr {get; set;}
    public string? CompanyNameEn {get; set;}
    public string? FullAddress {get; set;}
    public string? ContactNumber {get; set;}
    public string? Email {get; set;}
    public Guid? EstablishmentId {get; set;}
}