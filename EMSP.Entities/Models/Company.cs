using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSP.Entities.Models;

public class Company : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string? CompanyNameAr {get; set;}
    [Required]
    [StringLength(100)]
    public string? CompanyNameEn {get; set;}
    [StringLength(15)]
    public string? CompanyCode {get; set;}
    [StringLength(15)]
    public string? ShortAddress {get; set;}
    [StringLength(100)]
    public string? FullAddress {get; set;}
    [StringLength(15)]
    public string? ContactNumber {get; set;}
    [StringLength(50)]
    public string? Email {get; set;}
    [StringLength(50)]
    public string? VatNumber { get;set; }
    
    public Guid? EstablishmentId {get; set;}
    [ForeignKey(nameof(EstablishmentId))]
    public virtual Establishment? Establishment {get; set;}
    
    public virtual ICollection<Employee>? Employees {get; set;}
}