using Application._Common.Interfaces;
using Domain.Common;
using Domain.User.ValueObject;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUnitOfWork _uow;

    public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository,
        IUserRepository userRepository, ICurrentUserProvider currentUserProvider, IUnitOfWork uow)
    {
        _subscriptionRepository = subscriptionRepository;
        _userRepository = userRepository;
        _currentUserProvider = currentUserProvider;
        _uow = uow;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        
        if (currentUser.Id != request.UserId.Value)
        {
            return Error.Unauthorized(description: "User is forbidden for taking this action");
        }
        
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.UserId.Value));
        if (user is null) return Error.NotFound(description: "User not found");

        // Operations that remove the user from the current subscription
        if (user.Subscription != null)
        {
            Subscription oldSubscription = user.Subscription;

            // If user is in free subscription type (only supports one user), delete it
            if (user.Subscription.SubscriptionType.Name == SubscriptionType.Basic.Name)
            {
                user.LeaveSubscription();
                await _subscriptionRepository.DeleteAsync(oldSubscription);
            }
            // If user subscription type supports multiple users, delete user from it
            else
            {
                // If user is last on subscription, delete subscription
                bool deleteSubscription = user.Subscription.UserIds.Count <= 1;
                var removeUserResult = user.LeaveSubscription();
                if (removeUserResult.IsError) return removeUserResult.Errors;
                if (deleteSubscription) await _subscriptionRepository.DeleteAsync(oldSubscription);
            }
        }

        // Operations that link the user to the new created subscription
        ErrorOr<Subscription> subscription = Subscription.Create(request.SubscriptionType);
        if (subscription.IsError) return subscription.Errors;

        var updateUserResult = user.UpdateSubscription(subscription.Value);
        if (updateUserResult.IsError) return updateUserResult.Errors;

        await _subscriptionRepository.AddAsync(subscription.Value);
        await _userRepository.UpdateAsync(user);
        await _uow.CommitAsync();
        
        return subscription.Value;
    }
}