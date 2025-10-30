using Nexta.Application.DTO.Users;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Notifications.GetNotificationsQuery
{
    public class GetNotificationsQuery : IRequest<PagedData<NotificationDto>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}