using EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IHealthInsuranceService
{
    Task<List<HealthInsuranceSummaryResponse>> GetHealthInsurances();
    
    Task<HealthInsuranceSummaryResponse> AddHealthInsurance(HealthInsuranceAddRequest? healthInsuranceAddRequest);
    
    Task<HealthInsuranceDetailedResponse?> GetHealthInsuranceById(Guid? healthInsuranceId);
    
    Task<HealthInsuranceSummaryResponse> UpdateHealthInsurance(
        HealthInsuranceUpdateRequest? healthInsuranceUpdateRequest);
    
    Task SoftDeleteHealthInsurance(Guid healthInsuranceId);
}