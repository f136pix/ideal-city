using FluentValidation;

namespace Application.Cities.Commands.CreateCity;

public sealed class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Must(name => name != null).WithMessage("Name must be a string");

        RuleFor(x => x.CountryId)
            .NotEmpty().WithMessage("Country Id is required")
            .Matches(@"^[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}$")
            .WithMessage("Country Id should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)");
    }
}