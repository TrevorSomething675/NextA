using Nexta.Application.DTO;

namespace Nexta.Application.Commands.Auth.LoginCommand
{
    public class LoginCommandResponse(UserResponse user)
    {
		public UserResponse User { get; init; } = user;
	}
}