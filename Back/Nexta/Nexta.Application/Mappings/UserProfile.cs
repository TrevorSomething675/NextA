using Nexta.Domain.Entities;
using AutoMapper;
using Nexta.Domain.Models;
using Nexta.Application.Commands.AuthCommands.RegistrationCommand;

namespace Nexta.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegistrationCommandRequest, UserEntity>()
                .ForMember(src => src.PasswordHash, opt => opt.MapFrom(x => x.Password));

            CreateMap<UserEntity, User>().ReverseMap();
        }
    }
}