using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IEstablishmentService
{
    Task<List<EstablishmentDetailedResponse>> GetEstablishments();
    
    Task<EstablishmentDetailedResponse> AddEstablishment(EstablishmentAddRequest? establishmentAddRequest);
    
    Task<EstablishmentDetailedResponse?>  GetEstablishmentById(Guid? establishmentId);
    
    Task<EstablishmentDetailedResponse> UpdateEstablishment(EstablishmentUpdateRequest? establishmentUpdateRequest);
    
    Task<bool> SoftDeleteEstablishment(Guid? establishmentId);
}