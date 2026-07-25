namespace EMSP.Entities.Models;

public class HealthInsurance : BaseEntity
{
    public string? PolicyNumber {get; set;}
    public string? InsuranceProvider {get; set;}
    public DateTime? PolicyExpiryDate {get; set;}
    
    public Guid? EstablishmentId {get; set;}
    public virtual Establishment? Establishment {get; set;}
    
    public virtual ICollection<Employee>? Employees {get; set;}
}