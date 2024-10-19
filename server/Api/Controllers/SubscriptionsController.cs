using Application.Subscriptions.Commands;
using Application.Subscriptions.Commands.AddUserToSubscription;
using Contracts.Subscriptions;
using Contracts.Subscriptions.AddUserToSubscription;
using Contracts.Subsriptions;
using Domain.User.ValueObject;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionType = Domain.Common.SubscriptionType;

namespace Api.Controllers;

using DomainSubscriptionType = SubscriptionType;

[ApiController]
[Route("/api/users/{userId:guid}/subscriptions")]
public class SubscriptionsController : ApiController
{
    public SubscriptionsController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> CreateSubscription(Guid? userId, CreateSubscriptionRequest? request)
    {
        if (!DomainSubscriptionType.TryFromName(
                request.SubscriptionType.ToString(),
                out var subscriptionType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid subscription type");
        }

        CreateSubscriptionCommand command = _mapper.Map<CreateSubscriptionCommand>(request);
        command.UserId = userId;

        ErrorOr<Subscription> result = await Invoke<Subscription>(command);
        return result.Match(
            subscription => Ok(_mapper.Map<CreateSubscriptionResponse>(subscription)), 
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }

    [HttpPost("{subscriptionId:guid}")]
    // [Authorize]
    public async Task<IActionResult> AddUserToSubscription(AddUserToSubscriptionRequest request)
    {
        AddUserToSubscriptionCommand command = new AddUserToSubscriptionCommand(request.SubscriptionId);
        
        ErrorOr<Subscription> result = await Invoke<Subscription>(command);
        return result.Match(
            subscription => Ok(_mapper.Map<AddUserToSubscriptionResponse>(subscription)), 
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }
}