using Nexta.Application.Commands.Account.UpdateAccountCommand;
using Nexta.Application.Commands.Auth.RegisterCommand;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Response;
using Nexta.Application.DTO.Admin;
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
            
            CreateMap<User, AdminUserResponse>();
            CreateMap<PagedData<User>, PagedData<AdminUserResponse>>();
        }
    }
}