namespace EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;

public class EmployeeCostUpdateRequest
{
    public Guid Id {get; set;}
    public Guid? UpdatedBy {get; set;}
    public DateTime UpdatedAt {get; set;} =  DateTime.UtcNow;
    
    public string? CostType {get; set;}
    public string? Description {get; set;}
    public decimal CostAmount {get; set;}
    public DateTime? DueDate {get; set;}
    public bool IsPaid {get; set;}
    public DateTime? PaidDate {get; set;}
    public string? ReferenceNumber {get; set;}
    
    public Guid? EmployeeId {get; set;}
}