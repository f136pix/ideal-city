using Application.Users;
using Application.Users.Queries.GetUserSubscription;
using Contracts.Users;
using Domain.User.ValueObject;
using Domain.UserAggregate;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("/api/users")]
public class UserController : ApiController
{
    public UserController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    // getUser
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}/subscription")]
    public async Task<IActionResult> GetUserSubscription(string id)
    {
        GetUserSubscriptionQuery query = new GetUserSubscriptionQuery(id);
        ErrorOr<GetUserSubscriptionResponse> result = await Invoke<GetUserSubscriptionResponse>(query);
        return result.Match(
            subscription => Ok(subscription),
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateUser(
        UpdateUserRequest request
    )
    {
        UpdateUserCommand command = _mapper.Map<UpdateUserCommand>(request);
        ErrorOr<User> result = await Invoke<User>(command);
        return result.Match(
            user => CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<UserResponse>(user)),
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }
}