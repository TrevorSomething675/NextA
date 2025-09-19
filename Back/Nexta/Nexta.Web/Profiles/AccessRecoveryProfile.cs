using Nexta.Application.Commands.Auth.AccessRecoveryCommand;
using Nexta.Web.Areas.Models;
using AutoMapper;

namespace Nexta.Web.Profiles
{
    public class AccessRecoveryProfile : Profile
    {
        public AccessRecoveryProfile()
        {
            CreateMap<AccessRecoveryRequest, AccessRecoveryCommand>();
        }
    }
}
