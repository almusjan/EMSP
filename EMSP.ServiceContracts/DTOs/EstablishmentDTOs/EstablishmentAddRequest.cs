namespace EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

public class EstablishmentAddRequest
{
    public Guid? CreatedBy {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    
    public string? EstablishmentNameAr { get; set; }
    public string? EstablishmentNameEn { get; set; }
    public string? EstablishmentType {get; set;}
    public string? NationalId {get; set;}
    public string? FullAddress {get; set;}
}