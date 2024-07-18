using Application._Common.Interfaces;
using Application.Countries.Commands.AddCityToCountry;
using Domain.Cities.Events;
using Domain.CityAggregate;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace Application.Cities.Events;

public class CityCreatedHandler : INotificationHandler<CityCreated>
{
    private readonly IValidator<AddCityToCountryCommand> _addCityToCountryCommandValidator;
    private readonly IMapper _mapper;
    private readonly IApplicationService _service;

    public CityCreatedHandler(
        IApplicationService service,
        IMapper mapper,
        IValidator<AddCityToCountryCommand> addCityToCountryCommandValidator
    )
    {
        _service = service;
        _mapper = mapper;
        _addCityToCountryCommandValidator = addCityToCountryCommandValidator;
    }

    public async Task Handle(CityCreated notification, CancellationToken cancellationToken)
    {
        // AddCityToCountryCommand command = _mapper.Map<AddCityToCountryCommand>(notification);
        AddCityToCountryCommand command = new AddCityToCountryCommand(notification.City);
       
        var res = await _service.ValidateAndExecute(_addCityToCountryCommandValidator, command);
    }
}