using EMSP.ServiceContracts.DTOs.CountryDTOs;
using EMSP.ServiceContracts.Interfaces;

namespace EMSP.Services;

public class CountryService : ICountryService
{
    public async Task<List<CountryResponse>> GetCountries()
    {
        throw new NotImplementedException();
    }

    public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
    {
        throw new NotImplementedException();
    }
}