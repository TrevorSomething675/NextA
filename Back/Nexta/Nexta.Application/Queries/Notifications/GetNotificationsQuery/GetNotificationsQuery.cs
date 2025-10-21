using Nexta.Domain.Filters.Notifications;
using Nexta.Application.DTO.User;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Notifications.GetNotificationsQuery
{
    public class GetNotificationsQuery : IRequest<PagedData<NotificationDto>>
    {
        public GetNotificationsFilter Filter { get; init; } = null!;
    }
}