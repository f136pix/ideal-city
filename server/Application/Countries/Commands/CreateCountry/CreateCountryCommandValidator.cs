using Application.Cities.Commands.CreateCity;
using Application.Counties.Commands.CreateCountry;
using FluentValidation;

namespace Application.Countries.Commands.CreateCountry;

public sealed class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(x => x.Name)
            .Length(1, 50).WithMessage("Name must be between 1 and 50 characters long")
            .NotEmpty().WithMessage("Name is required")
            .Must(name => name != null).WithMessage("Name must be a string");
   }
    
}