using Domain.Common;

namespace Domain.Cities.Events;

public record CityCreated(CityAggregate.City City) : IDomainEvent;

