using BlazorHRM.Shared;

namespace BlazorHRM.Server.Models
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAllCountries();
        Country GetCountryById(int countryId);
    }
}