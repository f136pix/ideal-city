using Application.Authentication.Commands;
using Application.Authentication.Common;
using Contracts.Authentication;
using Contracts.Cities;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[Route("/api/auth")]
public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUser(
        RegisterUserRequest request
    )
    {
        RegisterUserCommand command = _mapper.Map<RegisterUserCommand>(request);
        ErrorOr<AuthenticationResult> result = await Invoke<AuthenticationResult>(command);
        return result.Match(
            city => Ok(value: _mapper.Map<RegisterResponse>(city)),
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }
}