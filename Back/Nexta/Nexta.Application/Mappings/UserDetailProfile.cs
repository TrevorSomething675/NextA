using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Mappings
{
    public class UserDetailProfile : Profile
    {
        public UserDetailProfile() 
        {
		    CreateMap<UserDetail, UserDetailEntity>().ReverseMap();
        }
    }
}