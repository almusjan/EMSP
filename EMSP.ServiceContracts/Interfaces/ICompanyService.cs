using EMSP.ServiceContracts.DTOs.CompanyDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface ICompanyService
{
    Task<List<CompanyDetailedResponse>> GetCompanies();
    
    Task<CompanyDetailedResponse> AddCompany(CompanyAddRequest? companyAddRequest);
    
    Task<CompanyDetailedResponse?> GetCompanyById(Guid? companyId);
    
    Task<CompanyDetailedResponse> UpdateCompany(CompanyUpdateRequest? companyUpdateRequest);
    
    //bool DeleteCompany(Guid? companyId);
}