using EMSP.ServiceContracts.DTOs.BankDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IBankService
{
    Task<List<BankResponse>> GetBanks();
    
    Task<BankResponse> AddBank(BankAddRequest? bankAddRequest);
}