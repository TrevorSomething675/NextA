using Nexta.Application.Commands.Account.UpdateAccountCommand;
using Nexta.Application.Commands.Auth.RegisterCommand;
using Nexta.Application.DTO.Response;
using Nexta.Application.DTO.Admin;
using AutoMapper;
using Nexta.Domain.Base;
using Nexta.Domain.Models.User;

namespace Nexta.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegisterCommand, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateAccountCommand, User>();
            
            CreateMap<User, AdminUserResponse>();
            CreateMap<PagedData<User>, PagedData<AdminUserResponse>>();
        }
    }
}