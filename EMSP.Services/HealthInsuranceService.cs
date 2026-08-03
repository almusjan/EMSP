using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services.Helpers;

namespace EMSP.Services;

public class HealthInsuranceService : IHealthInsuranceService
{
    private readonly IHealthInsuranceRepository _healthInsuranceRepository;

    public HealthInsuranceService(IHealthInsuranceRepository healthInsuranceRepository)
    {
        _healthInsuranceRepository = healthInsuranceRepository;
    }
    
    public async Task<List<HealthInsuranceSummaryResponse>> GetHealthInsurances()
    {
        List<HealthInsurance> healthInsurances = await _healthInsuranceRepository.GetAllAsync(hi => !hi.IsDeleted);
        
        return healthInsurances.Select(hi => hi.ToHealthInsuranceSummaryResponseObject()).ToList();
    }

    public async Task<HealthInsuranceSummaryResponse> AddHealthInsurance(
        HealthInsuranceAddRequest? healthInsuranceAddRequest)
    {
        if(healthInsuranceAddRequest == null)
            throw new ArgumentNullException(nameof(healthInsuranceAddRequest));
        
        ValidationHelper.ModelValidation(healthInsuranceAddRequest);

        HealthInsurance healthInsurance = healthInsuranceAddRequest.ToHealthInsuranceObject();
        healthInsurance.Id = Guid.NewGuid();
        
        await  _healthInsuranceRepository.AddAsync(healthInsurance);
        
        return healthInsurance.ToHealthInsuranceSummaryResponseObject();
    }

    public async Task<HealthInsuranceDetailedResponse?> GetHealthInsuranceById(Guid? healthInsuranceId)
    {
        if (healthInsuranceId == null)
            throw new ArgumentNullException(nameof(healthInsuranceId));
        
        HealthInsurance? healthInsurance = await _healthInsuranceRepository.GetByIdAsync(healthInsuranceId.Value);
        
        if(healthInsurance == null)
            throw new  KeyNotFoundException($"The healthInsurance with ID {healthInsuranceId} not found or soft-deleted");
        
        return healthInsurance.ToHealthInsuranceDetailedResponseObject();
    }

    public async Task<HealthInsuranceSummaryResponse> UpdateHealthInsurance(
        HealthInsuranceUpdateRequest? healthInsuranceUpdateRequest)
    {
        if(healthInsuranceUpdateRequest  == null)
            throw new ArgumentNullException(nameof(healthInsuranceUpdateRequest));
        
        ValidationHelper.ModelValidation(healthInsuranceUpdateRequest);
        
        HealthInsurance? matchingHealthInsurance = await _healthInsuranceRepository.GetByIdAsync(healthInsuranceUpdateRequest.Id);
        
        if(matchingHealthInsurance == null)
            throw new KeyNotFoundException($"The healthInsurance with ID {healthInsuranceUpdateRequest.Id} not found");

        #region CheckingUpdateFields

        matchingHealthInsurance.PolicyExpiryDate = healthInsuranceUpdateRequest.PolicyExpiryDate;
        matchingHealthInsurance.PolicyNumber = healthInsuranceUpdateRequest.PolicyNumber;
        matchingHealthInsurance.InsuranceProvider = healthInsuranceUpdateRequest.InsuranceProvider;
        matchingHealthInsurance.EstablishmentId = healthInsuranceUpdateRequest.EstablishmentId;

        #endregion
        
        await _healthInsuranceRepository.UpdateAsync(matchingHealthInsurance);

        return matchingHealthInsurance.ToHealthInsuranceSummaryResponseObject();
    }

    public async Task SoftDeleteHealthInsurance(Guid healthInsuranceId)
    {
        HealthInsurance? healthInsurance = await _healthInsuranceRepository.GetByIdAsync(healthInsuranceId);
        
        if(healthInsurance == null)
            throw new KeyNotFoundException($"The healthInsurance with ID {healthInsuranceId} not found");
        
        if(healthInsurance.IsDeleted)
            throw new InvalidOperationException("The healthInsurance is already soft deleted");
        
        healthInsurance.IsDeleted = true;
        
        await _healthInsuranceRepository.UpdateAsync(healthInsurance);
    }
}