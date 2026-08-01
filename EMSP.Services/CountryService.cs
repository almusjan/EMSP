using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.CountryDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services.Helpers;

namespace EMSP.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }
    
    public async Task<List<CountryResponse>> GetCountries()
    {
        List<Country> countries = await _countryRepository.GetAllAsync();

        return countries.Where(c => !c.IsDeleted).Select(c => c.ToCountryResponseObject()).ToList();
    }

    public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
    {
        if(countryAddRequest == null)
            throw new ArgumentNullException(nameof(countryAddRequest));
        
        ValidationHelper.ModelValidation(countryAddRequest);

        Country country = countryAddRequest.ToCountryObject();
        country.Id = Guid.NewGuid();
        
        await _countryRepository.AddAsync(country);
        
        return country.ToCountryResponseObject();
    }
}