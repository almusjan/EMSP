namespace EMSP.ServiceContracts.DTOs.CountryDTOs;

public class CountryResponse
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    
    public string? CountryNameAr {get; set;}
    public string? CountryNameEn {get; set;}
    public string? NationalityAr {get; set;}
    public string? NationalityEn {get; set;}
    public string? CountryCode {get; set;}
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        
        if(obj.GetType() != typeof(CountryResponse))
            return false;
        
        CountryResponse otherResponse = (CountryResponse)obj;
        
        return Id ==  otherResponse.Id && CountryCode == otherResponse.CountryCode;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    
    public override string ToString()
    {
        return $"[{CountryCode}] {CountryNameEn}|{CountryNameAr}";
    }
}