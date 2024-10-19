using Application._Common.Interfaces;
using Application.Cities.Commands.CreateCityRating;
using Domain.City.ValueObjects;
using Domain.CityAggregate;
using ErrorOr;
using MediatR;

namespace Application.Cities.Commands;

public class CreateCityReviewCommandHandler : IRequestHandler<CreateCityReviewCommand, ErrorOr<CityReview>>
{
    private readonly ICityRepository _cityRepository;
    
    public CreateCityReviewCommandHandler(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
    public async Task<ErrorOr<CityReview>> Handle(CreateCityReviewCommand request, CancellationToken cancellationToken)
    {
        City? city = await _cityRepository.GetByIdAsync(CityId.Create(request.CityId));

        if (city is null) return Error.NotFound(description:"City with given id not found");
        
        
        
        
        
        
        throw new NotImplementedException();
    }
}