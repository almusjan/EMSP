namespace EMSP.ServiceContracts.DTOs.SalaryDTOs;

public class SalaryResponse
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime? UpdatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    
    public decimal BasicSalary {get; set;}
    public decimal? TransportationAllowance {get; set;}
    public decimal? HousingAllowance {get; set;}
    public decimal? OtherAllowance {get; set;}
    public decimal TotalSalary {get; set;}
}