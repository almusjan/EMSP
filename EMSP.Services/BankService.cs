using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.BankDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services.Helpers;

namespace EMSP.Services;

public class BankService : IBankService
{
    private readonly IBankRepository  _bankRepository;

    public BankService(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }
    
    public async Task<List<BankResponse>> GetBanks()
    {
        List<Bank> banks = await _bankRepository.GetAllAsync();

        return banks.Where(b => !b.IsDeleted).Select(b => b.ToBankResponseObject()).ToList();
    }

    public async Task<BankResponse> AddBank(BankAddRequest? bankAddRequest)
    {
        if (bankAddRequest == null)
            throw new ArgumentNullException(nameof(bankAddRequest));
        
        ValidationHelper.ModelValidation(bankAddRequest);

        Bank bank = bankAddRequest.ToBankObject();
        bank.Id = Guid.NewGuid();
        
        await _bankRepository.AddAsync(bank);
        
        return bank.ToBankResponseObject();
    }
}