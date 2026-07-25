namespace EMSP.ServiceContracts.DTOs.CountryDTOs;

public class CountryResponse
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    
    public string? CountryNameAr {get; set;}
    public string? CountryNameEn {get; set;}
    public string? NationalityAr {get; set;}
    public string? NationalityEn {get; set;}
    public string? CountryCode {get; set;}
}