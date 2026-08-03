namespace EMSP.ServiceContracts.DTOs.CompanyDTOs;

public class CompanySummaryResponse
{
    public Guid Id {get; set;}
    public string? CompanyNameAr {get; set;}
    public string? CompanyNameEn {get; set;}
    public string? CompanyCode {get; set;}
    public string? ShortAddress {get; set;}
    public string? ContactNumber {get; set;}
    public string? Email {get; set;}
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        
        if(obj.GetType() != typeof(CompanySummaryResponse))
            return false;
        
        CompanySummaryResponse otherResponse = (CompanySummaryResponse)obj;
        
        return Id ==  otherResponse.Id && CompanyCode == otherResponse.CompanyCode;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    
    public override string ToString()
    {
        return $"[{CompanyCode}] {CompanyNameEn}|{CompanyNameAr}";
    }
}