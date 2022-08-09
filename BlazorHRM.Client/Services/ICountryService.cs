using System;
using BlazorHRM.Shared;

namespace BlazorHRM.Client.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> GetCountry(int countryId);
    }
}

