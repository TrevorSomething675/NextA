using Nexta.Application.DTO.Response;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;

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