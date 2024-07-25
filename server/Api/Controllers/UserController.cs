using Application.Users;
using Contracts.Users;
using Domain.UserAggregate;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("/api/users")]
public class UserController : ApiController
{
    protected UserController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    // getUser
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        throw new NotImplementedException();
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