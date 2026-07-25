namespace EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

public class EstablishmentSummaryResponse
{
    public Guid Id {get; set;}
    public string? EstablishmentNameAr { get; set; }
    public string? EstablishmentNameEn { get; set; }
    public string? EstablishmentCode { get; set; }
    public string? EstablishmentType {get; set;}
    public string? NationalId {get; set;}
    public string? ShortAddress {get; set;}
}