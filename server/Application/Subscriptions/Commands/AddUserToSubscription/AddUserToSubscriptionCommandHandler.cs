using Application._Common.Interfaces;
using Domain.Common;
using Domain.User.ValueObject;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.AddUserToSubscription;

public class AddUserToSubscriptionCommandHandler : IRequestHandler<AddUserToSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUnitOfWork _uow;

    public AddUserToSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository,
        IUserRepository userRepository, ICurrentUserProvider currentUserProvider, IUnitOfWork uow)
    {
        _subscriptionRepository = subscriptionRepository;
        _userRepository = userRepository;
        _currentUserProvider = currentUserProvider;
        _uow = uow;
    }


    public async Task<ErrorOr<Subscription>> Handle(AddUserToSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        User? user = await _userRepository.GetByIdAsync(UserId.Create(currentUser.Id));
        if (user is null) return Error.NotFound(description: "User not found");

        Subscription? subscription =
            await _subscriptionRepository.GetByIdAsync(SubscriptionId.Create(request.SubscriptionId));
        if (subscription is null) return Error.NotFound(description: "Subscription not found");

        if (user.Subscription.SubscriptionType.Name != SubscriptionType.Basic.Name)
        {
            return Error.Conflict(description:"User is already in a paid subscription type, leave it before going to a new subscription");
        }
        
        var result = subscription.AddUser(user);
        if (result.IsError)
        {
            return result.Errors.First();
        }

        var updatedUserResult = user.UpdateSubscription(subscription);
        if (updatedUserResult.IsError) return updatedUserResult.Errors.First();
        
        await _subscriptionRepository.UpdateAsync(subscription);
        await _userRepository.UpdateAsync(user);
        await _uow.CommitAsync();

        return subscription;
    }
}