namespace EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

public class EstablishmentUpdateRequest
{
    public Guid Id {get; set;}
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
    public Guid? UpdatedBy {get; set;}
    
    public string? EstablishmentNameAr { get; set; }
    public string? EstablishmentNameEn { get; set; }
    public string? EstablishmentCode { get; set; }
    public string? EstablishmentType {get; set;}
    public string? NationalId {get; set;}
    public string? CommercialRegistrationNumber {get; set;}
    public string? ShortAddress {get; set;}
    public string? FullAddress {get; set;}
    public string? VatNumber { get;set; }
}