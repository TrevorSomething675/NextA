using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.LoginCommand
{
	public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, Result<LoginCommandResponse>>
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

		public async Task<Result<LoginCommandResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var user = _mapper.Map<User>(await _userRepository.GetByEmailAsync(request.Email));
				if (user == null)
					return new Result<LoginCommandResponse>().Invalid("Пользователь не зарегистрирован");

				if (!_passwordHashService.Validate(request.Password, user.PasswordHash!))
					return new Result<LoginCommandResponse>().Invalid("Неверный логин или пароль");

				var accessToken = _jwtTokenService.CreateAccessToken(user.Id, user.Role.ToString());

				return new Result<LoginCommandResponse>(new LoginCommandResponse(user, accessToken, "RefreshToken"));
			}
			catch (Exception ex)
			{
				return new Result<LoginCommandResponse>().BadRequest(ex.Message);
			}
		}
	}
}