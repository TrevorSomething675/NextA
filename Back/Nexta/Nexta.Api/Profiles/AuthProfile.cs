using Nexta.Application.Commands.Auth.CheckAuthCommand;
using Nexta.Application.Commands.Auth.LoginCommand;
using Nexta.Web.Models.Auth;
using AutoMapper;
using Nexta.Application.Commands.Auth.RegistrationCommand;

namespace Nexta.Web.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegistrationRequest, RegistrationCommand>();

            CreateMap<LoginRequest, LoginCommand>();

            CreateMap<CheckUserAuthRequest, CheckAuthCommand>();
        }
    }
}
