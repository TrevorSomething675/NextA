using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserEntity>().ReverseMap();
            CreateMap<PagedData<UserEntity>, PagedData<User>>();
        }
    }
}