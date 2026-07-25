namespace EMSP.ServiceContracts.DTOs.SalaryDTOs;

public class SalaryAddRequest
{
    public Guid? CreatedBy {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public decimal BasicSalary {get; set;}
    public decimal? TransportationAllowance {get; set;}
    public decimal? HousingAllowance {get; set;}
    public decimal? OtherAllowance {get; set;}
    public Guid? EmployeeId { get; set; }
}