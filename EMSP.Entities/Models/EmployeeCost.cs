using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMSP.Entities.Enums;

namespace EMSP.Entities.Models;

public class EmployeeCost : BaseEntity
{
    public CostType? CostType {get; set;}
    [StringLength(200)]
    public string? Description {get; set;}
    public decimal CostAmount {get; set;}
    public DateTime? DueDate {get; set;}
    public bool IsPaid {get; set;}
    public DateTime? PaidDate {get; set;}
    [StringLength(50)]
    public string? ReferenceNumber {get; set;}
    
    public Guid? EmployeeId {get; set;}
    [ForeignKey(nameof(EmployeeId))]
    public virtual Employee? Employee {get; set;}
}