using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services.Helpers;

namespace EMSP.Services;

public class EstablishmentService : IEstablishmentService
{
    private readonly IEstablishmentRepository _establishmentRepository;

    public EstablishmentService(IEstablishmentRepository establishmentRepository)
    {
        _establishmentRepository = establishmentRepository;
    }
    
    public async Task<List<EstablishmentSummaryResponse>> GetEstablishments()
    {
        List<Establishment> establishments = await _establishmentRepository.GetAllAsync();

        return establishments.Where(e => !e.IsDeleted).Select(e => e.ToEstablishmentSummaryResponseObject()).ToList();
    }

    public async Task<EstablishmentSummaryResponse> AddEstablishment(EstablishmentAddRequest? establishmentAddRequest)
    {
        if(establishmentAddRequest == null)
            throw new ArgumentNullException(nameof(establishmentAddRequest));
        
        ValidationHelper.ModelValidation(establishmentAddRequest);

        Establishment establishment = establishmentAddRequest.ToEstablishmentObject();
        establishment.Id = Guid.NewGuid();
        
        await _establishmentRepository.AddAsync(establishment);
        
        return establishment.ToEstablishmentSummaryResponseObject();
    }

    public async Task<EstablishmentDetailedResponse?> GetEstablishmentById(Guid? establishmentId)
    {
        if (establishmentId == null)
            return null;
        
        Establishment?  establishment = await _establishmentRepository.GetByIdAsync(establishmentId.Value);

        if (establishment == null || establishment.IsDeleted)
            return null;
        
        return establishment.ToEstablishmentDetailedResponseObject();
    }

    public async Task<EstablishmentSummaryResponse> UpdateEstablishment(
        EstablishmentUpdateRequest? establishmentUpdateRequest)
    {
        if(establishmentUpdateRequest == null)
            throw new ArgumentNullException(nameof(establishmentUpdateRequest));
        
        ValidationHelper.ModelValidation(establishmentUpdateRequest);
        
        Establishment? matchingEstablishment =  await _establishmentRepository.GetByIdAsync(establishmentUpdateRequest.Id);
        
        if (matchingEstablishment == null)
            throw new KeyNotFoundException("Establishment not found");

        #region CheckingUpdateFields

        matchingEstablishment.EstablishmentNameAr =  establishmentUpdateRequest.EstablishmentNameAr;
        matchingEstablishment.EstablishmentNameEn = establishmentUpdateRequest.EstablishmentNameEn;
        matchingEstablishment.EstablishmentCode =  establishmentUpdateRequest.EstablishmentCode;
        matchingEstablishment.EstablishmentType = establishmentUpdateRequest.EstablishmentType;
        matchingEstablishment.NationalId =  establishmentUpdateRequest.NationalId;
        matchingEstablishment.CommercialRegistrationNumber =  establishmentUpdateRequest.CommercialRegistrationNumber;
        matchingEstablishment.ShortAddress =  establishmentUpdateRequest.ShortAddress;
        matchingEstablishment.FullAddress  =  establishmentUpdateRequest.FullAddress;
        matchingEstablishment.VatNumber =   establishmentUpdateRequest.VatNumber;

        #endregion
        
        await  _establishmentRepository.UpdateAsync(matchingEstablishment);
        
        return matchingEstablishment.ToEstablishmentSummaryResponseObject();
    }

    public async Task SoftDeleteEstablishment(Guid establishmentId)
    {
        Establishment? establishment = await _establishmentRepository.GetByIdAsync(establishmentId);
        
        if (establishment == null)
            throw new KeyNotFoundException("Establishment not found");
        
        if(establishment.IsDeleted)
            throw new InvalidOperationException("Establishment already soft-deleted");
        
        establishment.IsDeleted = true;
        
        await _establishmentRepository.UpdateAsync(establishment);
    }
}