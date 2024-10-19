using System.Reflection;
using Application._Common.Authorization;
using Application._Common.Interfaces;
using Contracts.Subscriptions;
using Domain.Common;
using ErrorOr;
using MediatR;
using SubscriptionType = Domain.Common.SubscriptionType;

namespace Application._Common.Behaviors;

public class AuthorizationBehavior<TRequset, TResponse> : IPipelineBehavior<TRequset, TResponse>
    where TRequset : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ICurrentUserProvider _currentUserProvider;

    public AuthorizationBehavior(ICurrentUserProvider currentUserProvider)
    {
        _currentUserProvider = currentUserProvider;
    }

    public async Task<TResponse> Handle(TRequset request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizationAttributes = request.GetType()
            .GetCustomAttributes<AuthorizeAttribute>()
            .ToList();

        if (authorizationAttributes.Count == 0)
        {
            return await next();
        }

        var currentUser = _currentUserProvider.GetCurrentUser();

        // if (authorizationAttributes.Any(authAttribute => !string.IsNullOrEmpty(authAttribute.Permissions)))
        // {
        //     var requiredPermissions = authorizationAttributes.SelectMany(authAttribute =>
        //         authAttribute.Permissions?.Split(',') ?? Array.Empty<string>()).ToList();
        //
        //     // If user does not have all the required perms
        //     if (requiredPermissions.Except(currentUser.Permissions).Any())
        //     {
        //         return (dynamic)Error.Unauthorized();
        //     }
        // }
        //
        // if (authorizationAttributes.Any(authAttribute => !string.IsNullOrEmpty(authAttribute.Roles)))
        // {
        //     var requiredRoles = authorizationAttributes.SelectMany(authAttribute =>
        //         authAttribute.Roles?.Split(',') ?? Array.Empty<string>()).ToList();
        //
        //     // If user does not have all the required roles
        //     if (requiredRoles.Except(currentUser.Roles).Any())
        //     {
        //         return (dynamic)Error.Unauthorized();
        //     }
        // }

          if (authorizationAttributes.Any(authAttribute =>
                !string.IsNullOrEmpty(authAttribute.Subscription.ToString())))
        {
            var requiredSubscription = authorizationAttributes.Select(a => a.Subscription).FirstOrDefault();

            SubscriptionType.TryFromValue((int)requiredSubscription, out var convertedRequiredSubscription);

            SubscriptionType.TryFromValue(currentUser.Subscription, out var convertedUserSubscription);

            if (convertedUserSubscription.Value < convertedRequiredSubscription.Value)
            {
                return (dynamic)Error.Unauthorized(
                    description: "User does not have the required subscription to access this resource");
            }
        }

        return await next();
    }
}