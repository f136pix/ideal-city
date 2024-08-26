using Contracts.Users;
using Domain.User.ValueObject;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetUserSubscription;

public record GetUserSubscriptionQuery(string UserId) : IRequest<ErrorOr<GetUserSubscriptionResponse>>;