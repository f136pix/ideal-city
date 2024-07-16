using Application._Common.Interfaces;
using Domain.Cities;
using Domain.City.ValueObjects;
using Domain.CityAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistance.Repositories;

public class CityRepository : ICityRepository
{
    private readonly IdealCityDbContext _dbContext;

    public CityRepository(IdealCityDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public new Task<IReadOnlyList<City>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<City> AddAsync(City entity)
    {
        var city = await _dbContext.AddAsync(entity);

        return city.Entity;
    }

    public Task<City> UpdateAsync(City entity)
    {
        throw new NotImplementedException();
    }

    public Task<City> GetByIdAsync(CityId id)
    {
        return _dbContext.Cities
            .Include(c => c.Country)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public City GetByIdSync(CityId id)
    {
        throw new NotImplementedException();
    }

    public Task<City> DeleteAsync(CityId id)
    {
        throw new NotImplementedException();
    }

    public Task<City> GetByProperty(string propertyName, string value)
    {
        throw new NotImplementedException();
    }
}