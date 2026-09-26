using Nexta.Application.Commands.Account.UpdateAccountCommand;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Response;
using Nexta.Application.DTO.Admin;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Application.Commands.Auth.RegistrationCommand;

namespace Nexta.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegistrationCommand, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateAccountCommand, User>();
            
            CreateMap<User, AdminUserResponse>();
            CreateMap<PagedData<User>, PagedData<AdminUserResponse>>();
        }
    }
}