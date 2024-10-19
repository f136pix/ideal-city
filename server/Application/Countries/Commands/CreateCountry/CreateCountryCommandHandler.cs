using Application._Common.Interfaces;
using Application.Counties.Commands.CreateCountry;
using Domain.CountryAggregate;
using ErrorOr;
using MediatR;

namespace Application.Countries.Commands.CreateCountry;

public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, ErrorOr<Country>>
{
    private readonly ICountryRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateCountryCommandHandler(ICountryRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }
    
    public async Task<ErrorOr<Country>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = Country.Create(request.Name);
        
        await _repository.AddAsync(country);

        await _uow.CommitAsync(); 
        
        return country;
    }
}