using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class UserDetailProfile : Profile
    {
        public UserDetailProfile() 
        {
            CreateMap<UserDetail, UserDetailResponse>();
        }
    }
}