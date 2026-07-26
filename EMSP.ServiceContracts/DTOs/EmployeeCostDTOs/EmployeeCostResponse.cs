using EMSP.Entities.Enums;

namespace EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;

public class EmployeeCostResponse
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    
    public CostType? CostType {get; set;}
    public string? Description {get; set;}
    public decimal CostAmount {get; set;}
    public DateTime? DueDate {get; set;}
    public bool IsPaid {get; set;}
    public DateTime? PaidDate {get; set;}
    public string? ReferenceNumber {get; set;}
}