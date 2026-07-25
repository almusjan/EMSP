namespace EMSP.ServiceContracts.DTOs.BankDTOs;

public class BankResponse
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public Guid? CreatedBy { get; set; }
    
    public string? BankNameAr {get; set;}
    public string? BankNameEn {get; set;}
    public string? BankCode {get; set;}
}