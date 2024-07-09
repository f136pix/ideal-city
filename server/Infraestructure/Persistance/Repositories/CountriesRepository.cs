using Application._Common.Interfaces;

namespace Infraestructure.Persistance.Repositories;

public class CountriesRepository  : ICommonRepository<Country>
{
    public Task<IReadOnlyList<Country>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Country> AddAsync(Country entity)
    {
        throw new NotImplementedException();
    }

    public Task<Country> UpdateAsync(Country entity)
    {
        throw new NotImplementedException();
    }

    public Task<Country> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Country GetByIdSync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Country> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Country GetByIdSync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Country> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Country> GetByProperty(string propertyName, string value)
    {
        throw new NotImplementedException();
    }
}