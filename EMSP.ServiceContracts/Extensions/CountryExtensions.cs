using EMSP.Entities.Models;
using EMSP.ServiceContracts.DTOs.CountryDTOs;

namespace EMSP.ServiceContracts.Extensions;

public static class CountryExtensions
{
    // Convert AddRequest to country object
    public static Country ToCountryObject(this CountryAddRequest countryAddRequest)
    {
        return new Country
        {
            CountryCode =  countryAddRequest.CountryCode,
            CountryNameAr =  countryAddRequest.CountryNameAr,
            CountryNameEn =  countryAddRequest.CountryNameEn,
            NationalityAr =   countryAddRequest.NationalityAr,
            NationalityEn =   countryAddRequest.NationalityEn
        };
    }
    
    // Convert country to CountryResponse object
    public static CountryResponse ToCountryResponseObject(this Country country)
    {
        return new CountryResponse()
        { 
            Id = country.Id,
            CreatedAt =  country.CreatedAt,
            
            CountryCode =  country.CountryCode,
            CountryNameAr =  country.CountryNameAr,
            CountryNameEn =  country.CountryNameEn,
            NationalityAr =  country.NationalityAr,
            NationalityEn =  country.NationalityEn
        };
    }
}