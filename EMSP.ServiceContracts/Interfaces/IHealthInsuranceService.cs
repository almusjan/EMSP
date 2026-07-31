using EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IHealthInsuranceService
{
    Task<List<HealthInsuranceDetailedResponse>> GetHealthInsurances();
    
    Task<HealthInsuranceDetailedResponse> AddHealthInsurance(HealthInsuranceAddRequest? healthInsuranceAddRequest);
    
    Task<HealthInsuranceDetailedResponse?> GetHealthInsuranceById(Guid? healthInsuranceId);
    
    Task<HealthInsuranceDetailedResponse> UpdateHealthInsurance(HealthInsuranceUpdateRequest? healthInsuranceUpdateRequest);
    
    Task<bool> SoftDeleteHealthInsurance(Guid? healthInsuranceId);
}