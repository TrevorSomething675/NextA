using Nexta.Application.Queries.Notifications.GetNotificationsQuery;
using Nexta.Web.Models.Notifications;
using AutoMapper;

namespace Nexta.Web.Mappings
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<GetNotificationsRequest, GetNotificationsQuery>();
        }
    }
}
