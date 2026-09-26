using MediatR;

namespace Nexta.Application.Commands.Auth.RegistrationCommand
{
    public class RegistrationCommand : IRequest<RegistrationCommandResponse>
    {
		public string Email { get; init; } = null!;
		public string FirstName { get; init; } = null!;
		public string MiddleName { get; init; } = null!;
		public string LastName { get; init; } = null!;
        public string Password { get; init; } = null!;
		public string ConfirmPassword { get; init; } = null!;

		public string Code { get; init; } = null!;
    }
}