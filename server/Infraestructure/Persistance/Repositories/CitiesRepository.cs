using Application._Common.Interfaces;
using Domain.Cities;
using Domain.City.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistance.Repositories;

public class CitiesRepository : ICommonRepository<City>
{
    private readonly IdealCityDbContext _dbContext;

    public CitiesRepository(IdealCityDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<IReadOnlyList<City>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<City> AddAsync(City entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;


    }

    public Task<City> UpdateAsync(City entity)
    {
        throw new NotImplementedException();
    }

    public async Task<City> GetByIdAsync(string id)
    {
        var guid = Guid.Parse(id);
        var cityId = CityId.Create(guid);
        var city = await _dbContext.Cities
            .Include(c => c.Country)
            .SingleOrDefaultAsync(c => c.Id == cityId);
        return city;
    }

    public City GetByIdSync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<City> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<City> GetByProperty(string propertyName, string value)
    {
        throw new NotImplementedException();
    }
}