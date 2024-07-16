using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application._Common.Interfaces;

public interface IApplicationService
{
    public Task<ErrorOr<T>> ValidateAndExecute<T>(IValidator validator, IRequest<ErrorOr<T>> command);
}