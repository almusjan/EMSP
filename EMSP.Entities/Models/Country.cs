using System.ComponentModel.DataAnnotations;

namespace EMSP.Entities.Models;

public class Country :  BaseEntity
{
    [Required]
    [StringLength(100)]
    public string? CountryNameAr {get; set;}
    [Required]
    [StringLength(100)]
    public string? CountryNameEn {get; set;}
    [StringLength(100)]
    public string? NationalityAr {get; set;}
    [StringLength(100)]
    public string? NationalityEn {get; set;}
    [StringLength(4)]
    public string? CountryCode {get; set;}
    
    public virtual ICollection<Employee>? Residents {get; set;}
}