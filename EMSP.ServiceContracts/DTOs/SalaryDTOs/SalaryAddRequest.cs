namespace EMSP.ServiceContracts.DTOs.SalaryDTOs;

public class SalaryAddRequest
{
    public decimal BasicSalary {get; set;}
    public decimal? TransportationAllowance {get; set;}
    public decimal? HousingAllowance {get; set;}
    public decimal? OtherAllowance {get; set;}
}