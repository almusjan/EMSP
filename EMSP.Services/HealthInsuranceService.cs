using EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;
using EMSP.ServiceContracts.Interfaces;

namespace EMSP.Services;

public class HealthInsuranceService : IHealthInsuranceService
{
    public async Task<List<HealthInsuranceDetailedResponse>> GetHealthInsurances()
    {
        throw new NotImplementedException();
    }

    public async Task<HealthInsuranceDetailedResponse> AddHealthInsurance(HealthInsuranceAddRequest? healthInsuranceAddRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<HealthInsuranceDetailedResponse?> GetHealthInsuranceById(Guid? healthInsuranceId)
    {
        throw new NotImplementedException();
    }

    public async Task<HealthInsuranceDetailedResponse> UpdateHealthInsurance(HealthInsuranceUpdateRequest? healthInsuranceUpdateRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SoftDeleteHealthInsurance(Guid? healthInsuranceId)
    {
        throw new NotImplementedException();
    }
}