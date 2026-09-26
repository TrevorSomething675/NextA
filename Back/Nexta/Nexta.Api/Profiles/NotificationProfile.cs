using Nexta.Application.Queries.Notifications.GetNotificationsQuery;
using Nexta.Domain.Filters.Notifications;
using Nexta.Web.Models.Notifications;
using AutoMapper;

namespace Nexta.Web.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<GetNotificationsRequest, GetNotificationsFilter>()
                .ForMember(src => src.SearchTerm, opt => opt.MapFrom(x => x.SearchTerm ?? ""));

            CreateMap<GetNotificationsRequest, GetNotificationsQuery>()
                .ForMember(src => src.Filter, opt => opt.MapFrom(x => x));
        }
    }
}
