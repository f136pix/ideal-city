using Application.Cities.Commands;
using Application.Cities.Commands.CreateCity;
using Application.Cities.Commands.CreateCityReview;
using Contracts.Cities;
using Contracts.Cities.Ratings;
using Domain.Cities;
using Domain.City.ValueObjects;
using Domain.CityAggregate;
using ErrorOr;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Api.Controllers;

[Route("/api/cities")]
public class CityController : ApiController
{
    public CityController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCity(CreateCityRequest request)
    {
        CreateCityCommand command = _mapper.Map<CreateCityCommand>(request);
        ErrorOr<City> result = await Invoke<City>(command);

        return result.Match(
            city => CreatedAtAction(nameof(GetCity), new { id = city.Id }, _mapper.Map<CityResponse>(city)),
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(int id)
    {
        throw new NotImplementedException();
    }


    [HttpPost("{cityId}/reviews")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCityReview(Guid cityId, CreateCityReviewRequest request)
    {
        request.CityId = cityId;
        CreateCityReviewCommand command = _mapper.Map<CreateCityReviewCommand>(request);
        ErrorOr<CityReview> result = await Invoke<CityReview>(command);

        return result.Match(
            review => CreatedAtAction(nameof(GetCityReview),
                new { cityId = review.CityId.Value, reviewId = review.Id.Value },
                new CreateCityReviewResponse(review.CityId.Value, review.Rating, review.Review)
            ),
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }

    [HttpGet("{cityId}/ratings/{reviewId}")]
    public async Task<IActionResult> GetCityReview(Guid cityId, Guid reviewId)
    {
        throw new NotImplementedException();
    }
}