namespace EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

public class EstablishmentSummaryResponse
{
    public Guid Id {get; set;}
    public string? EstablishmentNameAr { get; set; }
    public string? EstablishmentNameEn { get; set; }
    public string? EstablishmentCode { get; set; }
    public string? EstablishmentType {get; set;}
    public string? NationalId {get; set;}
    public string? ShortAddress {get; set;}
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        
        if(obj.GetType() != typeof(EstablishmentSummaryResponse))
            return false;
        
        EstablishmentSummaryResponse otherResponse = (EstablishmentSummaryResponse)obj;
        
        return Id ==  otherResponse.Id && NationalId == otherResponse.NationalId;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    
    public override string ToString()
    {
        return $"[{NationalId}] {EstablishmentNameEn}|{EstablishmentNameAr}";
    }
}