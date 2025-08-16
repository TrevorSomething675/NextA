using Nexta.Application.Commands.Auth.RegistrationCommand;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Application.DTO.Response;

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