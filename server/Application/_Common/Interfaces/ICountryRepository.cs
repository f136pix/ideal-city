using Domain.Countries;
using Domain.CountryAggregate;

namespace Application._Common.Interfaces;

public interface ICountryRepository : IRepository<Country>
{
    Task<Country> GetByIdAsync(CountryId id);
    Country GetByIdSync(CountryId id);
    Task<Country> DeleteAsync(CountryId id);
    public Task<Country> GetByProperty(string propertyName, string value);
}