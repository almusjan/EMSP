using EMSP.ServiceContracts.DTOs.CompanyDTOs;
using EMSP.Entities.Models;

namespace EMSP.ServiceContracts.Extensions;

public static class CompanyExtensions
{
    // Convert AddRequest to Company Object
    public static Company ToCompanyObject(this CompanyAddRequest companyAddRequest)
    {
        return new Company()
        {
            CompanyNameAr =  companyAddRequest.CompanyNameAr,
            CompanyNameEn = companyAddRequest.CompanyNameEn,
            FullAddress =  companyAddRequest.FullAddress,
            ContactNumber =   companyAddRequest.ContactNumber,
            Email =   companyAddRequest.Email,
            EstablishmentId =    companyAddRequest.EstablishmentId
        };
    }
    
    // Convert Company to CompanySummaryResponse Object
    public static CompanySummaryResponse ToCompanySummaryResponseObject(this Company company)
    {
        return new CompanySummaryResponse()
        {
            Id = company.Id,
            CompanyNameAr = company.CompanyNameAr,
            CompanyNameEn = company.CompanyNameEn,
            CompanyCode = company.CompanyCode,
            ShortAddress = company.ShortAddress,
            ContactNumber = company.ContactNumber,
            Email = company.Email
        };
    }
    
    // Convert Company to CompanyDetailedResponse Object
    public static CompanyDetailedResponse ToCompanyDetailedResponseObject(this Company company)
    {
        return new CompanyDetailedResponse()
        {
            Id = company.Id,
            UpdatedAt =  company.UpdatedAt,
            UpdatedBy =   company.UpdatedBy,
            CreatedAt =   company.CreatedAt,
            CreatedBy =   company.CreatedBy,
            
            CompanyCode =  company.CompanyCode,
            CompanyNameAr =  company.CompanyNameAr,
            CompanyNameEn = company.CompanyNameEn,
            ShortAddress =  company.ShortAddress,
            FullAddress =  company.FullAddress,
            ContactNumber = company.ContactNumber,
            Email = company.Email,
            VatNumber =  company.VatNumber,
            
            // est. dto
            Establishment = company.Establishment?.ToEstablishmentSummaryResponseObject(),
            
            // list of employees dto
            Employees = company.Employees?.Select(e => e.ToEmployeeSummaryResponseObject()).ToList()
        };
    }
}