using Nexta.Application.DTO.Response;
using AutoMapper;
using Nexta.Domain.Base;
using Nexta.Domain.Models.User;

namespace Nexta.Application.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationResponse>();
            CreateMap<PagedData<Notification>, PagedData<NotificationResponse>>();
        }
    }
}