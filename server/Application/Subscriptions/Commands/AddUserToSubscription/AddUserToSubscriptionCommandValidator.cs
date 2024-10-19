using Domain.Common;
using FluentValidation;

namespace Application.Subscriptions.Commands.AddUserToSubscription;

public class AddUserToSubscriptionCommandValidator : AbstractValidator<AddUserToSubscriptionCommand>
{

    public AddUserToSubscriptionCommandValidator()
    {
        RuleFor(x => x.SubscriptionId)
            .NotEmpty().WithMessage("Subscription ID is required");
    }
    
}