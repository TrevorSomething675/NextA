using Nexta.Application.DTO;

namespace Nexta.Application.Commands.Auth.RegistrationCommand
{
    public class RegistrationCommandResponse(UserResponse user)
    {
		public UserResponse User { get; init; } = user;
	}
}