using Application.Cities.Commands.CreateCity;
using Contracts.Cities;
using Domain.Cities;
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
    private readonly IMapper _mapper;

    public CityController(IMapper mapper, ISender mediator) : base(mediator)
    {
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCity(
        CreateCityRequest request
    )
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
}