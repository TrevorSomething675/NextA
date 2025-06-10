using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.LoginCommand
{
	public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
	{
		private readonly IPasswordHashService _passwordHashService;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public LoginCommandHandler(IPasswordHashService passwordHashService, IUserRepository userRepository, 
			IMapper mapper, IJwtTokenService jwtTokenService)
		{
			_passwordHashService = passwordHashService;
			_jwtTokenService = jwtTokenService;
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken ct)
		{
			var user = _mapper.Map<User>(await _userRepository.GetByEmailAsync(request.Email, ct));
			if (user == null)
				throw new NotFoundException("Пользователь не зарегистрирован");

			if (!_passwordHashService.Validate(request.Password, user.PasswordHash!))
				throw new UnauthorizedException("Неверный логин или пароль");

			var accessToken = _jwtTokenService.CreateAccessToken(user.Id, user.Role.ToString());

			return new LoginCommandResponse(user, accessToken, "RefreshToken");
		}
	}
}