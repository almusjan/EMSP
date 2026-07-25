namespace EMSP.Entities.Models;

public class Establishment : BaseEntity
{
    public string? EstablishmentNameAr { get; set; }
    public string? EstablishmentNameEn { get; set; }
    public string? EstablishmentCode { get; set; }
    public string? EstablishmentType {get; set;}
    public string? NationalId {get; set;}
    public string? CommercialRegistrationNumber {get; set;}
    public string? ShortAddress {get; set;}
    public string? FullAddress {get; set;}
    public string? VatNumber { get;set; }
        
    public virtual ICollection<Employee>? Employees {get; set;}
    public virtual ICollection<Company>? Companies {get; set;}
    public virtual ICollection<HealthInsurance>? HealthInsurances {get; set;}
}