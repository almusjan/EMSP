using EMSP.ServiceContracts.DTOs.CountryDTOs;

namespace EMSP.ServiceContracts.Interfaces;

public interface ICountryService
{
    Task<List<CountryResponse>> GetCountries();
    
    Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);
}