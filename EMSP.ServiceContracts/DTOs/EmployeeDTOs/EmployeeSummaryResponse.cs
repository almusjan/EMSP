using EMSP.Entities.Enums;

namespace EMSP.ServiceContracts.DTOs.EmployeeDTOs;

public class EmployeeSummaryResponse
{
    public Guid Id {get; set;}
    public string? FullNameAr { get; set; }
    public string? FullNameEn { get; set; }
    public string? IqamaOrIdNumber {get; set;}
    public DateTime? DateOfBirth { get; set; }
    public GenderOptions?  Gender {get; set;}
    public string? PhoneNumber { get; set; }
    public string? CountryCode { get; set; }
    public string? Profession {get; set;}
    public DateTime? HireDate {get; set;}
    public string? EstablishmentCode { get; set; }
    public string? CompanyCode { get; set; }
    public bool? HasMemberPolicyNumber {get; set;}

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        
        if(obj.GetType() != typeof(EmployeeSummaryResponse))
            return false;
        
        EmployeeSummaryResponse otherResponse = (EmployeeSummaryResponse)obj;
        
        return Id ==  otherResponse.Id && IqamaOrIdNumber == otherResponse.IqamaOrIdNumber;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    
    public override string ToString()
    {
        return $"{IqamaOrIdNumber} - {FullNameEn}|{FullNameAr}";
    }
}