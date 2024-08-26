using Application.Subscriptions.Commands;
using Contracts.Subsriptions;
using Domain.Common;
using Domain.User.ValueObject;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [Authorize]
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
            subscription => Ok(subscription), 
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }

    [HttpPost("{subscriptionId:guid}")]
    public async Task<IActionResult> AddUserToSubscription(Guid userId, Guid subscriptionId)
    {
        throw new NotImplementedException();
        // AddUserToSubscriptionCommand command = new AddUserToSubscriptionCommand(userId, subscriptionId);
        // ErrorOr<Unit> result = await Invoke<Unit>(command);
        // return result.Match(
        //     _ => Ok(),
        //     errors => Problem(errors)
        // ) ?? Problem("An unexpected error occurred");
    }
}