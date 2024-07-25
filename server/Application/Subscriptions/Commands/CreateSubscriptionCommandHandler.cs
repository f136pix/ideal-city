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
    private readonly IUnitOfWork _uow;

    public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository,
        IUserRepository userRepository, IUnitOfWork uow)
    {
        _subscriptionRepository = subscriptionRepository;
        _userRepository = userRepository;
        _uow = uow;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.UserId.Value));
        if (user is null) return Error.NotFound(description: "User not found");

        ErrorOr<Subscription> subscription = Subscription.Create(request.SubscriptionType);
        if (subscription.IsError) return subscription.Errors;

        var updateResult = user.UpdateSubscription(subscription.Value);
        if (updateResult.IsError) return updateResult.Errors;

        await _subscriptionRepository.AddAsync(subscription.Value);
        await _userRepository.UpdateAsync(user);
        await _uow.CommitAsync();

        return subscription;
    }
}