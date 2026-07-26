using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;
using EMSP.ServiceContracts.Interfaces;

namespace EMSP.Services;

public class EstablishmentService : IEstablishmentService
{
    public async Task<List<EstablishmentDetailedResponse>> GetEstablishments()
    {
        throw new NotImplementedException();
    }

    public async Task<EstablishmentDetailedResponse> AddEstablishment(EstablishmentAddRequest? establishmentAddRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<EstablishmentDetailedResponse?> GetEstablishmentById(Guid? establishmentId)
    {
        throw new NotImplementedException();
    }

    public async Task<EstablishmentDetailedResponse> UpdateEstablishment(EstablishmentUpdateRequest? establishmentUpdateRequest)
    {
        throw new NotImplementedException();
    }
}