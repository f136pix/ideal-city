using Application._Common.Interfaces;
using Application._Common.Interfaces.Authentication;
using Application.Authentication.Common;
using Domain.Common;
using Domain.User;
using Domain.User.ValueObject;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUnitOfWork _uow;

    public RegisterUserCommandHandler(IUserRepository userRepository, ISubscriptionRepository subscriptionRepository,
        IJwtTokenGenerator tokenGenerator, IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _subscriptionRepository = subscriptionRepository;
        _tokenGenerator = tokenGenerator;
        _uow = uow;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken
    )
    {
        if (await _userRepository.GetByProperty("Email", request.Email) is User user)
            return Error.Validation(description: "Email already exists");

        ErrorOr<Subscription> newSubscription = Subscription.Create(SubscriptionType.Basic);

        User newUser = User.Create(request.Name, request.Email, newSubscription.Value, null,
            request.Password, null, null);
        string token = _tokenGenerator.GenerateToken(newUser.Id.Value, newUser.Email);

        // ErrorOr<Success> addResult = newSubscription.AddUser(newUser);
        // if (addResult.IsError) return addResult.Errors;

        await _subscriptionRepository.AddAsync(newSubscription.Value);
        await _userRepository.AddAsync(newUser);
        await _uow.CommitAsync();

        return new AuthenticationResult(newUser, token);
    }
}