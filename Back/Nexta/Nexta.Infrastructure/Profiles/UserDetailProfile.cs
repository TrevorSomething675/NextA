using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class UserDetailProfile : Profile
    {
        public UserDetailProfile() 
        {
            CreateMap<UserDetail, UserDetailEntity>().ReverseMap();
        }
    }
}