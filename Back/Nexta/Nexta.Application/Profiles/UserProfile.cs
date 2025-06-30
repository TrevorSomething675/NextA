using Nexta.Application.Commands.Auth.RegistrationCommand;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegistrationCommandRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}