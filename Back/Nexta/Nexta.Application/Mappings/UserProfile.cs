using Nexta.Application.Commands.Auth.RegisterCommand;
using Nexta.Application.Commands.Auth.VerifyCodeCommand;
using Nexta.Application.DTO.Users;
using Nexta.Domain.Models.Users;
using AutoMapper;

namespace Nexta.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDto, RegisterCommandResponse>();
            CreateMap<UserDto, VerifyCodeCommandResponse>();
        }
    }
}
