namespace EMSP.ServiceContracts.DTOs.CompanyDTOs;

public class CompanyUpdateRequest
{
    public Guid Id  {get; set;}
    public Guid? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public string? CompanyNameAr {get; set;}
    public string? CompanyNameEn {get; set;}
    public string? CompanyCode {get; set;}
    public string? ShortAddress {get; set;}
    public string? FullAddress {get; set;}
    public string? ContactNumber {get; set;}
    public string? Email {get; set;}
    public string? VatNumber { get;set; }
    
    public Guid? EstablishmentId { set; get; }
    
}