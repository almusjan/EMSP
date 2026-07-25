namespace EMSP.Entities.Models;

public class Country :  BaseEntity
{
    public string? CountryNameAr {get; set;}
    public string? CountryNameEn {get; set;}
    public string? NationalityAr {get; set;}
    public string? NationalityEn {get; set;}
    public string? CountryCode {get; set;}
    
    public virtual ICollection<Employee>? Residents {get; set;}
}