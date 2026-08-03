using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;
using EMSP.Entities.Models;

namespace EMSP.ServiceContracts.Extensions;

public static class EstablishmentExtensions
{
    // Convert Add Request to establishment object
    public static Establishment ToEstablishmentObject(this EstablishmentAddRequest establishmentAddRequest)
    {
        return new Establishment()
        {
            EstablishmentNameAr = establishmentAddRequest.EstablishmentNameAr,
            EstablishmentNameEn = establishmentAddRequest.EstablishmentNameEn,
            EstablishmentType = establishmentAddRequest.EstablishmentType,
            NationalId = establishmentAddRequest.NationalId,
            FullAddress = establishmentAddRequest.FullAddress
        };
    }
    
    // Convert Establishment to summary response object
    public static EstablishmentSummaryResponse ToEstablishmentSummaryResponseObject(this Establishment establishment)
    {
        return new EstablishmentSummaryResponse()
        {
            Id = establishment.Id,
            EstablishmentNameAr = establishment.EstablishmentNameAr,
            EstablishmentNameEn = establishment.EstablishmentNameEn,
            EstablishmentCode = establishment.EstablishmentCode,
            EstablishmentType = establishment.EstablishmentType,
            NationalId = establishment.NationalId,
            ShortAddress = establishment.ShortAddress,
        };
    }
    
    // Convert Establishment to detailed response object
    public static EstablishmentDetailedResponse ToEstablishmentDetailedResponseObject(this Establishment establishment)
    {
        return new EstablishmentDetailedResponse()
        {
            Id = establishment.Id,
            CreatedAt = establishment.CreatedAt,
            CreatedBy = establishment.CreatedBy,
            UpdatedAt = establishment.UpdatedAt,
            UpdatedBy = establishment.UpdatedBy,
            
            EstablishmentNameAr = establishment.EstablishmentNameAr,
            EstablishmentNameEn = establishment.EstablishmentNameEn,
            EstablishmentCode = establishment.EstablishmentCode,
            EstablishmentType = establishment.EstablishmentType,
            NationalId = establishment.NationalId,
            CommercialRegistrationNumber = establishment.CommercialRegistrationNumber,
            ShortAddress = establishment.ShortAddress,
            FullAddress = establishment.FullAddress,
            VatNumber = establishment.VatNumber,
            
            // lists
            Companies = establishment.Companies?.Select(c => c.ToCompanySummaryResponseObject()).ToList(),
            HealthInsurances = establishment.HealthInsurances?.Select(hi => hi.ToHealthInsuranceSummaryResponseObject()).ToList(),
            Employees = establishment.Employees?.Select(e => e.ToEmployeeSummaryResponseObject()).ToList()
        };
    }
    
    // Only for unit test
    public static EstablishmentUpdateRequest ToEstablishmentUpdateRequest(
        this EstablishmentSummaryResponse establishmentSummaryResponse)
    {
        return new EstablishmentUpdateRequest()
        {
            Id = establishmentSummaryResponse.Id,
            EstablishmentCode =  establishmentSummaryResponse.EstablishmentCode,
            EstablishmentType = establishmentSummaryResponse.EstablishmentType,
            NationalId = establishmentSummaryResponse.NationalId,
            EstablishmentNameAr =  establishmentSummaryResponse.EstablishmentNameAr,
            EstablishmentNameEn =  establishmentSummaryResponse.EstablishmentNameEn,
            ShortAddress = establishmentSummaryResponse.ShortAddress
        };
    }
 }