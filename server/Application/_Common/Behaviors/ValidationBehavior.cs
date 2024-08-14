using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application._Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IServiceProvider _serviceProvider;
    private IValidator<TRequest>? _validator;

    public ValidationBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(typeof(TRequest));
        var _validator = _serviceProvider.GetService(validatorType) as IValidator<TRequest>;

        
        if (_validator is null)
        {
            Console.WriteLine($"No validator found for {typeof(TRequest)}");
            return await next();
        }
        
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            return await next();
        }

        List<Error> errors = validationResult
            .Errors
            .Select(error => Error.Validation(description: error.ErrorMessage, code: error.PropertyName))
            .ToList();

        return (dynamic)errors;
    }
}