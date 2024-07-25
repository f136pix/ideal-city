using Domain.Common;
using Domain.User.ValueObject;
using FluentValidation;

namespace Application.Subscriptions.Commands;

public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        int[] paidTypes =
        {
            SubscriptionType.Basic.Value,
            SubscriptionType.Premium.Value,
        };

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("City Id is required");

        RuleFor(x => x.SubscriptionType)
            .NotEmpty().WithMessage("Subscription type is required")
            .Must(x => paidTypes.Contains(x.Value)).WithMessage("Can't create Free subscription type since is default");
    }
}