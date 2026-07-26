using EMSP.ServiceContracts.DTOs.BankDTOs;
using EMSP.ServiceContracts.Interfaces;

namespace EMSP.Services;

public class BankService : IBankService
{
    public async Task<List<BankResponse>> GetBanks()
    {
        throw new NotImplementedException();
    }

    public async Task<BankResponse> AddBank(BankAddRequest? bankAddRequest)
    {
        throw new NotImplementedException();
    }
}