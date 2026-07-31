using System.ComponentModel.DataAnnotations;

namespace EMSP.Entities.Models;

public class Bank : BaseEntity
{
    [StringLength(100)]
    public string? BankNameAr {get; set;}
    [StringLength(100)]
    public string? BankNameEn {get; set;}
    
    public virtual ICollection<Employee>? Employees {get; set;}
}