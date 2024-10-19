using Application._Common.Authorization;
using Domain.Common;
using Domain.User.ValueObject;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.RemoveUserFromSubscription;

// Does the unsubscribing using the user Id provided in the jwt
[Authorize(Subscription = SubscriptionTypeEnum.Basic)]
public record RemoveUserFromSubscriptionCommand() : IRequest<ErrorOr<Subscription>>;
    