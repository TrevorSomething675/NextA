using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Application.DTO.Response;
using Nexta.Application.Commands.Auth.RegisterCommand;

namespace Nexta.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegisterCommand, User>();
            CreateMap<User, UserResponse>();
        }
    }
}