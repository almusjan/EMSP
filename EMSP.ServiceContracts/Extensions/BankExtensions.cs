using EMSP.ServiceContracts.DTOs.BankDTOs;
using EMSP.Entities.Models;

namespace EMSP.ServiceContracts.Extensions;

public static class BankExtensions
{
    // Convert AddRequest to Bank Object
    public static Bank ToBankObject(this BankAddRequest bankAddRequest)
    {
        return new Bank()
        {
            BankNameAr =  bankAddRequest.BankNameAr,
            BankNameEn = bankAddRequest.BankNameEn
        };
    }
    
    // Convert Bank to BankResponse Object
    public static BankResponse ToBankResponseObject(this Bank bank)
    {
        return new BankResponse()
        {
            Id =  bank.Id,
            CreatedAt =  bank.CreatedAt,
            BankNameAr = bank.BankNameAr,
            BankNameEn = bank.BankNameEn
        };
    }
}