using Application._Common.Interfaces;
using Application._Common.Interfaces.Authentication;
using Application.Authentication.Common;
using Domain.Common;
using Domain.User;
using Domain.User.ValueObject;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _uow;

    public RegisterUserCommandHandler(IUserRepository userRepository, ISubscriptionRepository subscriptionRepository,
        IJwtTokenGenerator tokenGenerator, IUnitOfWork uow, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _subscriptionRepository = subscriptionRepository;
        _tokenGenerator = tokenGenerator;
        _uow = uow;
        _passwordHasher = passwordHasher;
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
        if(newSubscription.IsError) return newSubscription.Errors;

        var hashPasswodResult = _passwordHasher.HashPassword(null, request.Password);

        User newUser = User.Create(request.Name, request.Email, newSubscription.Value, null,
            hashPasswodResult, null, null);

        var result = newSubscription.Value.AddUser(newUser);
        if (result.IsError) return result.Errors;

        string token = _tokenGenerator.GenerateToken(newUser.Id.Value, newUser.Email, newSubscription.Value);

        await _subscriptionRepository.AddAsync(newSubscription.Value);
        await _userRepository.AddAsync(newUser);
        await _uow.CommitAsync();

        return new AuthenticationResult(newUser, token);
    }
}