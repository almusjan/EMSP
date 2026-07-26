using EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IHealthInsuranceService
{
    Task<List<HealthInsuranceDetailedResponse>> GetHealthInsurances();
    
    Task<HealthInsuranceDetailedResponse> AddHealthInsurance(HealthInsuranceAddRequest? healthInsuranceAddRequest);
    
    Task<HealthInsuranceDetailedResponse?> GetHealthInsurance(Guid? healthInsuranceId);
    
    Task<HealthInsuranceDetailedResponse> UpdateHealthInsurance(HealthInsuranceUpdateRequest? healthInsuranceUpdateRequest);
    
    // bool DeleteHealthInsurance(Guid? healthInsuranceId);
}