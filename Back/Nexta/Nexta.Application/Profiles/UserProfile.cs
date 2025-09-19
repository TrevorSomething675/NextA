using Nexta.Application.Commands.Account.UpdateAccountCommand;
using Nexta.Application.Commands.Auth.RegisterCommand;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegisterCommand, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateAccountCommand, User>();
        }
    }
}