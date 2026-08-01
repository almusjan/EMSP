using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface IEstablishmentService
{
    Task<List<EstablishmentSummaryResponse>> GetEstablishments();
    
    Task<EstablishmentSummaryResponse> AddEstablishment(EstablishmentAddRequest? establishmentAddRequest);
    
    Task<EstablishmentDetailedResponse?>  GetEstablishmentById(Guid? establishmentId);
    
    Task<EstablishmentSummaryResponse> UpdateEstablishment(EstablishmentUpdateRequest? establishmentUpdateRequest);
    
    Task SoftDeleteEstablishment(Guid establishmentId);
}