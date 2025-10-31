using MediatR;

namespace Nexta.Application.Commands.Auth.SendVerifyCodeCommand
{
    public class SendVerifyCodeCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}