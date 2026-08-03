namespace EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;

public class HealthInsuranceSummaryResponse
{
    public Guid Id {get; set;}
    public string? PolicyNumber {get; set;}
    public string? InsuranceProvider {get; set;}
    public DateTime? PolicyExpiryDate {get; set;}
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        
        if(obj.GetType() != typeof(HealthInsuranceSummaryResponse))
            return false;
        
        HealthInsuranceSummaryResponse otherResponse = (HealthInsuranceSummaryResponse)obj;
        
        return Id ==  otherResponse.Id && PolicyNumber == otherResponse.PolicyNumber;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    
    public override string ToString()
    {
        return $"[{PolicyNumber}] {InsuranceProvider}|{PolicyExpiryDate}";
    }
}