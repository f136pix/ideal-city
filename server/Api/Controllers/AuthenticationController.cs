using Application.Authentication.Commands;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
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

    [HttpPost("/register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUser(
        RegisterUserRequest request
    )
    {
        RegisterUserCommand command = _mapper.Map<RegisterUserCommand>(request);
        ErrorOr<AuthenticationResult> result = await Invoke<AuthenticationResult>(command);
        return result.Match(
            ret => Ok(value: _mapper.Map<RegisterResponse>(ret)),
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    }
    
    [HttpPost("/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginUser(
        LoginUserRequest request
    )
    {
        LoginUserQuery command = _mapper.Map<LoginUserQuery>(request);
        ErrorOr<AuthenticationResult> result = await Invoke<AuthenticationResult>(command);
        return result.Match(
            ret => Ok(value: _mapper.Map<RegisterResponse>(ret)),
            errors => Problem(errors)
        ) ?? Problem("An unexpected error occurred");
    } 
    
}