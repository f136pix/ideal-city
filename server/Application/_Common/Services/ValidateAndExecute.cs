using Application._Common.Interfaces;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application._Common.Services;

public partial class ApplicationCommonService : IApplicationService
{
    private readonly IMediator _mediator;

    public ApplicationCommonService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ErrorOr<T>> ValidateAndExecute<T>(IValidator validator, IRequest<ErrorOr<T>> command)
    {
        var context = new ValidationContext<IRequest<ErrorOr<T>>>(command);
        var validationResult = await validator.ValidateAsync(context);
        if (!validationResult.IsValid)
        {
            List<Error> errors = FormatValidationErrors(validationResult.Errors);
            return errors;
        }

        return await _mediator.Send(command);
    }

    private List<Error> FormatValidationErrors(List<ValidationFailure> validationFailures)
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