using System.ComponentModel.DataAnnotations.Schema;

namespace EMSP.Entities.Models;

public class Salary : BaseEntity
{
    public decimal BasicSalary {get; set;}
    public decimal? TransportationAllowance {get; set;}
    public decimal? HousingAllowance {get; set;}
    public decimal? OtherAllowance {get; set;}
    
    // Calculate in business logic
    public decimal TotalSalary {get; set;}
    
    public Guid?  EmployeeId {get; set;}
}