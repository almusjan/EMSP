using EMSP.ServiceContracts.DTOs.CompanyDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface ICompanyService
{
    Task<List<CompanySummaryResponse>> GetCompanies();
    
    Task<CompanySummaryResponse> AddCompany(CompanyAddRequest? companyAddRequest);
    
    Task<CompanyDetailedResponse?> GetCompanyById(Guid? companyId);
    
    Task<CompanySummaryResponse> UpdateCompany(CompanyUpdateRequest? companyUpdateRequest);
    
    Task<bool> SoftDeleteCompany(Guid? companyId);
}