namespace EMSP.ServiceContracts.DTOs.BankDTOs;

public class BankAddRequest
{
    public DateTime CreatedAt {get; set;}
    
    public string? BankNameAr {get; set;}
    public string? BankNameEn {get; set;}
    public string? BankCode {get; set;}
}