using System.Diagnostics;
using Application._Common.Interfaces;
using Domain.City.ValueObjects;
using Domain.CityAggregate;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Users;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
{
    IUserRepository _repository;
    ICityRepository _cityRepository;

    public UpdateUserCommandHandler(IUserRepository repository, ICityRepository cityRepository)
    {
        _repository = repository;
        _cityRepository = cityRepository;
    }

    public async Task<ErrorOr<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        City? city = null;
        if (request.CityId is not null)
        {
            city = await _cityRepository.GetByIdAsync(CityId.Create(request.CityId.Value));
            if (city is null) return Error.NotFound(description: "City not found");
        }

        var user = await _repository.GetByIdAsync(UserId.Create(request.UserId));
        if (user is null) return Error.NotFound(description: "UserId not found");

        var updateResult = user.Update(
            name: request.Name,
            email: request.Email,
            city: city,
            password: request.Password,
            profilePicture: request.ProfilePicture,
            bio: request.Bio
        );

        if (updateResult.IsError) return updateResult.Errors;

        await _repository.UpdateAsync(user);
        return user;
    }
}