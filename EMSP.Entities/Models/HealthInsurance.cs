using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSP.Entities.Models;

public class HealthInsurance : BaseEntity
{
    [StringLength(100)]
    public string? PolicyNumber {get; set;}
    [StringLength(100)]
    public string? InsuranceProvider {get; set;}
    public DateTime? PolicyExpiryDate {get; set;}
    
    public Guid? EstablishmentId {get; set;}
    [ForeignKey(nameof(EstablishmentId))]
    public virtual Establishment? Establishment {get; set;}
    
    public virtual ICollection<Employee>? Employees {get; set;}
}