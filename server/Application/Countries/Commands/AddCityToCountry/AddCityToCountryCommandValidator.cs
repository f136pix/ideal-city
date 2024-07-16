using FluentValidation;

namespace Application.Countries.Commands.AddCityToCountry;

public class AddCityToCountryCommandValidator : AbstractValidator<AddCityToCountryCommand>
{
    public AddCityToCountryCommandValidator()
    {
        RuleFor(x => x.City.Id)
            .NotEmpty().WithMessage("City Id is required");

        RuleFor(x => x.City.Country.Id)
            .NotEmpty().WithMessage("Country Id is required");
    }
}