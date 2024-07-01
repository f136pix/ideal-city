using Application.Cities.Commands.CreateCity;
using Contracts.Cities;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers;

[ApiController]
// [Authorize]
public class ApiController : ControllerBase
{
    protected readonly ISender Mediator;

    protected ApiController(ISender mediator)
    {
        Mediator = mediator;
    }

    public async Task<ErrorOr<T>> Invoke<T>(IRequest<ErrorOr<T>> command)
    {
        var validationResult = await ValidateRequest(command);
        if (validationResult is not null)
        {
            return validationResult;
        }

        ErrorOr<T> result;

        try
        {
            result = await Mediator.Send(command);
        }
        catch (Exception e)
        {
            // result = Error.Failure(e.Message);
            result = Error.Failure("An unexpected error occurred");
        }

        return result;
    }

    protected ActionResult? Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ReturnValidationProblem(errors);
        }

        return ReturnProblem(errors[0]);
    }

    private ObjectResult? ReturnProblem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private ActionResult? ReturnValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        errors.ForEach(error => modelStateDictionary.AddModelError(error.Code, error.Description));

        return ValidationProblem(modelStateDictionary);
    }


    protected async Task<List<Error>?> ValidateRequest<T>(IRequest<T> request)
    {
        var commandType = request.GetType();
        var validatorType = typeof(IValidator<>).MakeGenericType(commandType);

        //     var validator = HttpContext.RequestServices.GetService<IValidator<commandType>>();
        dynamic validator = HttpContext.RequestServices.GetService(validatorType);

        if (validator == null)
        {
            List<Error> errors = new List<Error> { Error.NotFound(description: "Validator not found") };
            return errors;
        }

        ValidationResult validationResult = await validator.ValidateAsync((dynamic)request);
        if (!validationResult.IsValid)
        {
            List<Error> errors = FormatValidationErrors(validationResult.Errors);
            return errors;
        }

        return null;
    }

    protected List<Error> FormatValidationErrors(List<ValidationFailure> validationFailures)
    {
        List<Error> validationErrors = new List<Error>();

        foreach (var error in validationFailures)
        {
            var validationError = Error.Validation(
                description: error.ErrorMessage,
                code: error.PropertyName
            );
            validationErrors.Add(validationError);
        }

        return validationErrors;
    }
}