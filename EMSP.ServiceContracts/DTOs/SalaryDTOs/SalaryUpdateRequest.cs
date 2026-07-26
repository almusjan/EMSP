namespace EMSP.ServiceContracts.DTOs.SalaryDTOs;

public class SalaryUpdateRequest
{
    public Guid Id {get; set;}
    
    public decimal BasicSalary {get; set;}
    public decimal? TransportationAllowance {get; set;}
    public decimal? HousingAllowance {get; set;}
    public decimal? OtherAllowance {get; set;}
    public Guid? EmployeeId { get; set; }
}