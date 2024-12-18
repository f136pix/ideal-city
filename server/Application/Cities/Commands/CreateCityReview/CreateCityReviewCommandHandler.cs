using Application._Common.Interfaces;
using Application.Cities.Commands.CreateCityReview;
using Domain.City.ValueObjects;
using Domain.CityAggregate;
using ErrorOr;
using MediatR;

namespace Application.Cities.Commands;

public class CreateCityReviewCommandHandler : IRequestHandler<CreateCityReviewCommand, ErrorOr<CityReview>>
{
    private readonly ICityRepository _cityRepository;
    private readonly IUnitOfWork _uow;

    public CreateCityReviewCommandHandler(ICityRepository cityRepository, IUnitOfWork uow)
    {
        _cityRepository = cityRepository;
        _uow = uow;
    }
    public async Task<ErrorOr<CityReview>> Handle(CreateCityReviewCommand request, CancellationToken cancellationToken)
    {
        City? city = await _cityRepository.GetByIdAsync(CityId.Create(request.CityId));
        if (city is null) return Error.NotFound(description:"City with given id not found");
        
        CityReview review = CityReview.Create(request.Review, request.Rating);

        city.AddReview(review);

        await _uow.CommitAsync();

        return review;
    }
}