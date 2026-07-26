using EMSP.ServiceContracts.DTOs.CompanyDTOs;
using EMSP.ServiceContracts.Interfaces;

namespace EMSP.Services;

public class CompanyService : ICompanyService
{
    public async Task<List<CompanyDetailedResponse>> GetCompanies()
    {
        throw new NotImplementedException();
    }

    public async Task<CompanyDetailedResponse> AddCompany(CompanyAddRequest? companyAddRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<CompanyDetailedResponse?> GetCompanyById(Guid? companyId)
    {
        throw new NotImplementedException();
    }

    public async Task<CompanyDetailedResponse> UpdateCompany(CompanyUpdateRequest? companyUpdateRequest)
    {
        throw new NotImplementedException();
    }
}