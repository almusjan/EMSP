namespace EMSP.ServiceContracts.DTOs.BankDTOs;

public class BankResponse
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    
    public string? BankNameAr {get; set;}
    public string? BankNameEn {get; set;}
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        
        if(obj.GetType() != typeof(BankResponse))
            return false;
        
        BankResponse otherResponse = (BankResponse)obj;
        
        return Id ==  otherResponse.Id && BankNameAr == otherResponse.BankNameEn &&  BankNameEn == otherResponse.BankNameAr;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    
    public override string ToString()
    {
        return $"{BankNameEn}|{BankNameAr}";
    }
}