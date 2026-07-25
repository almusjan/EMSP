namespace EMSP.Entities.Models;

public class Bank : BaseEntity
{
    public string? BankNameAr {get; set;}
    public string? BankNameEn {get; set;}
    public string? BankCode {get; set;}
    
    public virtual ICollection<Employee>? Employees {get; set;}
}