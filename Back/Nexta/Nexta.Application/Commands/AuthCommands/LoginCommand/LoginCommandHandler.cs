using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;
using FluentValidation;

namespace Nexta.Application.Commands.AuthCommands.LoginCommand
{
	public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
	{
		private readonly IValidator<LoginCommandRequest> _validator;
		private readonly IHashService _passwordHashService;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public LoginCommandHandler(IHashService passwordHashService, IUserRepository userRepository,
			IMapper mapper, IValidator<LoginCommandRequest> validator)
		{
			_passwordHashService = passwordHashService;
			_userRepository = userRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if (!validationResult.IsValid)
				throw new BadRequestException(string.Join(", ", validationResult.Errors));

			var user = _mapper.Map<User>(await _userRepository.GetByEmailAsync(request.Email, ct));

			if (user == null)
				throw new NotFoundException("Пользователь не зарегистрирован");

			if (!_passwordHashService.Validate(request.Password, user.PasswordHash!))
				throw new UnauthorizedException("Неверный логин или пароль");

			return new LoginCommandResponse(user);
		}
	}
}