using Nexta.Domain.Filters.Notifications;
using MediatR;

namespace Nexta.Application.Queries.Notifications.GetNotificationsQuery
{
    public class GetNotificationsQuery : IRequest<GetNotificationsQueryResponse>
    {
        public GetNotificationsFilter Filter { get; init; } = null!;
    }
}