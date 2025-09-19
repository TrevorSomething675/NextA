using Nexta.Application.DTO.Response;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;

namespace Nexta.Application.Queries.Notifications.GetNotificationsQuery
{
    public class GetNotificationsQueryResponse : BasePagedResponse<NotificationResponse>
    {
        public GetNotificationsQueryResponse(PagedData<NotificationResponse> data) : base(data) { }
    }
}
