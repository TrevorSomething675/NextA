using Nexta.Application.DTO.Response;
using Nexta.Application.Common;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Notifications.GetNotificationsQuery
{
    public class GetNotificationsQueryResponse : BasePagedResponse<NotificationResponse>
    {
        public GetNotificationsQueryResponse(PagedData<NotificationResponse> data) : base(data) { }
    }
}
