using Application._Common.Interfaces;
using Domain.Common;
using Domain.User.ValueObject;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.RemoveUserFromSubscription;

public class
    RemoveUserFromSubscriptionCommandHandler : IRequestHandler<RemoveUserFromSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;

    public RemoveUserFromSubscriptionCommandHandler(ICurrentUserProvider currentUserProvider, IUserRepository userRepository, IUnitOfWork uow)
    {
        _currentUserProvider = currentUserProvider;
        _userRepository = userRepository;
        _uow = uow;
    }

    public async Task<ErrorOr<Subscription>> Handle(RemoveUserFromSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser is null)
        {
            return Error.Unauthorized(description: "User is forbidden for taking this action");
        }

        User? user = await _userRepository.GetByIdAsync(UserId.Create(currentUser.Id));
        if (user is null) return Error.NotFound(description: "User not found");

        if (user.Subscription.SubscriptionType.Value <= SubscriptionType.Basic.Value)
        {
            return Error.Conflict(description:"User is already in a free subscription type");
        }
        
        // Leaves and add user to a new Free tier subscription
        var result = user.LeaveSubscription();
        if (result.IsError) return Error.Failure(description: "There was a error leaving the subscription");

        await _userRepository.UpdateAsync(user);
        await _uow.CommitAsync();

        return user.Subscription;
    }
}