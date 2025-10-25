using MediatR;

namespace Nexta.Application.Commands.Auth.VerifyCodeCommand
{
    public class VerifyCodeCommand : IRequest<VerifyCodeCommandResponse>
    {
        public string Email { get; init; }
        public string Role { get; init; }
        public string Code { get; init; }
    }
}