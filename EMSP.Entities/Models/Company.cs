namespace EMSP.Entities.Models;

public class Company : BaseEntity
{
    public string? CompanyNameAr {get; set;}
    public string? CompanyNameEn {get; set;}
    public string? CompanyCode {get; set;}
    public string? ShortAddress {get; set;}
    public string? FullAddress {get; set;}
    public string? ContactNumber {get; set;}
    public string? Email {get; set;}
    public string? VatNumber { get;set; }
    
    public Guid? EstablishmentId {get; set;}
    public virtual Establishment? Establishment {get; set;}
    
    public virtual ICollection<Employee>? Employees {get; set;}
}