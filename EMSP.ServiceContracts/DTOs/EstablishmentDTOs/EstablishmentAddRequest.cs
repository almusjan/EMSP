namespace EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

public class EstablishmentAddRequest
{
    public string? EstablishmentNameAr { get; set; }
    public string? EstablishmentNameEn { get; set; }
    public string? EstablishmentType {get; set;}
    public string? NationalId {get; set;}
    public string? FullAddress {get; set;}
}