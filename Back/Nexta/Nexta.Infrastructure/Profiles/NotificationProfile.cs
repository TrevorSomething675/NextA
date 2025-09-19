using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationEntity>().ReverseMap();

            CreateMap<PagedData<Notification>, PagedData<NotificationEntity>>().ReverseMap();
        }
    }
}