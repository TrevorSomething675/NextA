using Nexta.Application.Commands.Account.ChangePasswordCommand;
using Nexta.Application.Commands.Account.UpdateAccountCommand;
using Nexta.Application.Commands.Account.UpdateEmailCommand;
using Nexta.Web.Models.Account;
using Nexta.Web.Areas.Models;
using AutoMapper;

namespace Nexta.Web.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<ChangePasswordRequest, ChangePasswordCommand>();
            CreateMap<UpdateAccountRequest, UpdateAccountCommand>();
            CreateMap<UpdateEmailRequest, UpdateEmailCommand>();
        }
    }
}
