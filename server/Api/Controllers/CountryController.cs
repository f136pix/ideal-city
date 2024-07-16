using Application.Counties.Commands.CreateCountry;
using Application.Countries.Queries.GetCountryByName;
using Contracts.Countries;
using Domain.CountryAggregate;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("/api/countries")]
public class CountryController : ApiController
{
    public CountryController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCountryById(int id)
    {
        throw new NotImplementedException();
        //     GetCountryByIdQuery query = new(id);
        //     ErrorOr<Country> result = await Invoke<Country>(query);
        //     return result.Match(
        //         country => Ok(_mapper.Map<CountryResponse>(country)),
        //         errors => Problem(errors)
        //     ) ?? Problem("An unexpected error occurred");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCountryByName([FromQuery] string name)
    {
        GetCountryByNameQuery query = new(name);
        ErrorOr<Country> result = await Invoke<Country>(query);
        return result.Match(
            country => Ok(_mapper.Map<CountryResponse>(country)),
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCountry(
        CreateCountryRequest request
    )
    {
        CreateCountryCommand command = _mapper.Map<CreateCountryCommand>(request);
        ErrorOr<Country> result = await Invoke<Country>(command);
        return result.Match(
            country => CreatedAtAction(nameof(GetCountryById), new { id = country.Id },
                _mapper.Map<CountryResponse>(country)),
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }
}