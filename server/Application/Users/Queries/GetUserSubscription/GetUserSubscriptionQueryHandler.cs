using Application._Common.Interfaces;
using Contracts.Users;
using Domain.User.ValueObject;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetUserSubscription;

public class GetUserSubscriptionQueryHandler : IRequestHandler<GetUserSubscriptionQuery, ErrorOr<GetUserSubscriptionResponse>>
{
    private ISubscriptionRepository _subscriptionRepository;
    private IUserRepository _userRepository;

    public GetUserSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository,
        IUserRepository userRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<GetUserSubscriptionResponse>> Handle(GetUserSubscriptionQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(UserId.Create(new Guid(request.UserId)));
        if (user is null)
        {
            return Error.NotFound(description: "No user found with the provided id");
        }

        // return user.Subscription;
        
        var subscription = await _subscriptionRepository.GetByIdAsync(user.SubscriptionId);
        if (subscription is null)
        {
            return Error.NotFound(description: "No subscription found for the provided user");
        }
        
        var response = new GetUserSubscriptionResponse
        {
            Id = subscription.Id.Value.ToString(),
            SubscriptionType = subscription.SubscriptionType.ToString(),
            ExpirationDate = subscription.ExpirationDate,
            IsActive = subscription.IsActive
        };

        return response;
    }
}