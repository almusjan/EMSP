using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.CompanyDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services.Helpers;

namespace EMSP.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task<List<CompanySummaryResponse>> GetCompanies()
    {
        List<Company> companies = await _companyRepository.GetAllAsync();

        return companies.Where(c => !c.IsDeleted).Select(c => c.ToCompanySummaryResponseObject()).ToList();
    }

    public async Task<CompanySummaryResponse> AddCompany(CompanyAddRequest? companyAddRequest)
    {
        if(companyAddRequest == null)
            throw new ArgumentNullException(nameof(companyAddRequest));
        
        ValidationHelper.ModelValidation(companyAddRequest);
        
        // next layer of validation - what's Unique in company?
        
        Company company = companyAddRequest.ToCompanyObject();
        company.Id =  Guid.NewGuid();
        
        await _companyRepository.AddAsync(company);
        
        return company.ToCompanySummaryResponseObject();
    }

    public async Task<CompanyDetailedResponse?> GetCompanyById(Guid? companyId)
    {
        if (companyId == null)
            return null;
        
        Company? company = await _companyRepository.GetByIdAsync(companyId.Value);

        if (company == null || company.IsDeleted)
            return null;
        
        return company.ToCompanyDetailedResponseObject();
    }

    public async Task<CompanySummaryResponse> UpdateCompany(CompanyUpdateRequest? companyUpdateRequest)
    {
        if(companyUpdateRequest == null)
            throw new  ArgumentNullException(nameof(companyUpdateRequest));
        
        ValidationHelper.ModelValidation(companyUpdateRequest);
        
        Company?  matchingCompany = await _companyRepository.GetByIdAsync(companyUpdateRequest.Id);

        if (matchingCompany == null)
            throw new KeyNotFoundException($"Company with id {companyUpdateRequest.Id} not found!");
        
        #region CheckingUpdateFields

        matchingCompany.UpdatedAt = DateTime.UtcNow;
        
        matchingCompany.CompanyNameAr = companyUpdateRequest.CompanyNameAr;
        matchingCompany.CompanyNameEn = companyUpdateRequest.CompanyNameEn;
        matchingCompany.CompanyCode =  companyUpdateRequest.CompanyCode;
        matchingCompany.ShortAddress = companyUpdateRequest.ShortAddress;
        matchingCompany.FullAddress =  companyUpdateRequest.FullAddress;
        matchingCompany.ContactNumber = companyUpdateRequest.ContactNumber;
        matchingCompany.Email = companyUpdateRequest.Email;
        matchingCompany.VatNumber = companyUpdateRequest.VatNumber;
        matchingCompany.EstablishmentId = companyUpdateRequest.EstablishmentId;

        #endregion
        
        await  _companyRepository.UpdateAsync(matchingCompany);
        
        return matchingCompany.ToCompanySummaryResponseObject();
    }

    public async Task<bool> SoftDeleteCompany(Guid? companyId)
    {
        if (companyId == null)
            return false;

        Company? matchingCompany = await _companyRepository.GetByIdAsync(companyId.Value);

        if (matchingCompany == null)
            return false;
        
        matchingCompany.IsDeleted = true;
        
        await _companyRepository.UpdateAsync(matchingCompany);

        return true;
    }
    // I may add 2 more methods - get soft deleted list - hard delete for companies who was hidden for some time
}