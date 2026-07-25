using EMSP.Entities.Models;
using EMSP.ServiceContracts.DTOs.BankDTOs;

namespace EMSP.ServiceContracts.Extensions;

public static class BankExtensions
{
    // Convert AddRequest to Bank Object
    public static Bank ToBankObject(this BankAddRequest bankAddRequest)
    {
        return new Bank()
        {
            BankNameAr =  bankAddRequest.BankNameAr,
            BankNameEn = bankAddRequest.BankNameEn,
            BankCode =  bankAddRequest.BankCode
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
            BankNameEn = bank.BankNameEn,
            BankCode = bank.BankCode
        };
    }
}