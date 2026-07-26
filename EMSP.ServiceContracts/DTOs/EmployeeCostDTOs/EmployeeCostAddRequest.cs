using EMSP.Entities.Enums;

namespace EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;

public class EmployeeCostAddRequest
{
    public CostType? CostType {get; set;}
    public string? Description {get; set;}
    public decimal CostAmount {get; set;}
    public DateTime? DueDate {get; set;}
    public string? ReferenceNumber {get; set;}
    public Guid? EmployeeId {get; set;}
}