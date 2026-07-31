using System.ComponentModel.DataAnnotations;

namespace EMSP.Entities.Models;

public class Establishment : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string? EstablishmentNameAr { get; set; }
    [Required]
    [StringLength(100)]
    public string? EstablishmentNameEn { get; set; }
    [StringLength(15)]
    public string? EstablishmentCode { get; set; }
    [StringLength(100)]
    public string? EstablishmentType {get; set;}
    [StringLength(50)]
    public string? NationalId {get; set;}
    [StringLength(50)]
    public string? CommercialRegistrationNumber {get; set;}
    [StringLength(15)]
    public string? ShortAddress {get; set;}
    [StringLength(100)]
    public string? FullAddress {get; set;}
    [StringLength(50)]
    public string? VatNumber { get;set; }
        
    public virtual ICollection<Employee>? Employees {get; set;}
    public virtual ICollection<Company>? Companies {get; set;}
    public virtual ICollection<HealthInsurance>? HealthInsurances {get; set;}
}