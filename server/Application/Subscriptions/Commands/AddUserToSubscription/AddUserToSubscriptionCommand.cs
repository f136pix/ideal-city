using Application._Common.Authorization;
using Domain.Common;
using Domain.User.ValueObject;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.AddUserToSubscription;

[Authorize(Subscription = SubscriptionTypeEnum.Basic)]
public record AddUserToSubscriptionCommand(Guid SubscriptionId) : IRequest<ErrorOr<Subscription>>, IRequest<Subscription>;