using Application._Common.Authorization;
using Domain.City.ValueObjects;
using Domain.Common;
using ErrorOr;
using MediatR;

namespace Application.Cities.Commands.CreateCityReview;

[Authorize(Subscription = SubscriptionTypeEnum.Basic)]
public record CreateCityReviewCommand(Guid CityId, int Rating, string Review) : IRequest<ErrorOr<CityReview>>;
