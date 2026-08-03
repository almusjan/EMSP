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
        List<Company> companies = await _companyRepository.GetAllAsync(c => !c.IsDeleted);

        return companies.Select(c => c.ToCompanySummaryResponseObject()).ToList();
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
            throw new ArgumentNullException(nameof(companyId));
        
        Company? company = await _companyRepository.GetByIdAsync(companyId.Value);

        if (company == null || company.IsDeleted)
            throw new KeyNotFoundException($"Company with ID {companyId} not found or soft-deleted");
        
        return company.ToCompanyDetailedResponseObject();
    }

    public async Task<CompanySummaryResponse> UpdateCompany(CompanyUpdateRequest? companyUpdateRequest)
    {
        if(companyUpdateRequest == null)
            throw new  ArgumentNullException(nameof(companyUpdateRequest));
        
        ValidationHelper.ModelValidation(companyUpdateRequest);
        
        Company?  matchingCompany = await _companyRepository.GetByIdAsync(companyUpdateRequest.Id);

        if (matchingCompany == null)
            throw new KeyNotFoundException($"Company with ID {companyUpdateRequest.Id} not found!");
        
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

    public async Task SoftDeleteCompany(Guid companyId)
    {
        Company? matchingCompany = await _companyRepository.GetByIdAsync(companyId);

        if (matchingCompany == null)
            throw new  KeyNotFoundException($"The company with ID {companyId} not found!");
        
        if(matchingCompany.IsDeleted)
            throw new InvalidOperationException("The company is already soft-deleted!");
        
        matchingCompany.IsDeleted = true;
        
        await _companyRepository.UpdateAsync(matchingCompany);
    }
    // I may add 2 more methods - get soft deleted list - hard delete for companies who was hidden for some time
}