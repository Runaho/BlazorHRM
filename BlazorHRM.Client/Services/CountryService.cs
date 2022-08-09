using System;
using BlazorHRM.Shared;

namespace BlazorHRM.Client.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;

        public CountryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await new ServiceRequestHelper<IEnumerable<Country>>(_httpClient).GetAsync("Countries");
        }

        public async Task<Country> GetCountry(int countryId)
        {
            return await new ServiceRequestHelper<Country>(_httpClient).GetAsync("Country", $"?id={countryId}");
        }
    }
}

