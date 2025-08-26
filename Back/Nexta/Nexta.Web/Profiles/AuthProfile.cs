using Nexta.Application.Commands.Auth.CheckAuthCommand;
using Nexta.Application.Commands.Auth.RegisterCommand;
using Nexta.Application.Commands.Auth.LoginCommand;
using Nexta.Web.Models.Auth;
using AutoMapper;

namespace Nexta.Web.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegistrationRequest, RegisterCommand>();

            CreateMap<LoginRequest, LoginCommand>();

            CreateMap<CheckUserAuthRequest, CheckAuthCommand>();
        }
    }
}
