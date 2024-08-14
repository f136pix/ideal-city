using Domain.CityAggregate;
using ErrorOr;
using MediatR;

namespace Application.Cities.Commands.CreateCity;

public class CreateCityCommandBehavior : IPipelineBehavior<CreateCityCommand, ErrorOr<City>>
{
    public async Task<ErrorOr<City>> Handle(CreateCityCommand request,
        RequestHandlerDelegate<ErrorOr<City>> next,
        CancellationToken cancellationToken)
    {
        // Sample of how we could be validating in the Behavior

        // var validator = new CreateCityCommandValidator();
        //
        // var validationResult = await validator.ValidateAsync(request);
        // if (!validationResult.IsValid)
        // {
        //     return validationResult.Errors
        //         .Select(err => Error.Validation(code: err.ErrorCode, description: err.ErrorMessage))
        //         .ToList();
        // }
        //
        // return await next();

        return await next();
    }
}