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
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        
        if(obj.GetType() != typeof(SalaryResponse))
            return false;
        
        SalaryResponse otherResponse = (SalaryResponse)obj;
        
        return Id ==  otherResponse.Id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    
    public override string ToString()
    {
        return $"{BasicSalary} / {HousingAllowance} | {TransportationAllowance} | {OtherAllowance} \\ | {TotalSalary}";
    }
}