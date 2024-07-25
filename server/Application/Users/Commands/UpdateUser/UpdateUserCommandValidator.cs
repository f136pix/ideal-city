using Application._Common.Validation;
using FluentValidation;

namespace Application.Users;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required")
            .Must(GuidValidatorAttribute.IsValidGuid).WithMessage("User Id must be a valid Guid");

        RuleFor(x => x.CityId)
            .Must(GuidValidatorAttribute.IsValidGuid).WithMessage("City Id must be a valid Guid");

    }
}


// public class AddCityToCountryCommandValidator : AbstractValidator<AddCityToCountryCommand>
// {
//     public AddCityToCountryCommandValidator()
//     {
//         RuleFor(x => x.City.Id)
//             .NotEmpty().WithMessage("City Id is required");
//
//         RuleFor(x => x.City.Country.Id)
//             .NotEmpty().WithMessage("Country Id is required");
//     }
// }