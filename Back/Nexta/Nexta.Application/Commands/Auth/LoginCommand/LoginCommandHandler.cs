using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using FluentValidation;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Auth.LoginCommand
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
	{
		private readonly IValidator<LoginCommand> _validator;
		private readonly IHashService _passwordHashService;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public LoginCommandHandler(IHashService passwordHashService, IUserRepository userRepository,
			IMapper mapper, IValidator<LoginCommand> validator)
		{
			_passwordHashService = passwordHashService;
			_userRepository = userRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(request, ct);
			if (!validationResult.IsValid)
				throw new BadRequestException(string.Join(", ", validationResult.Errors));

			var user = await _userRepository.GetByEmailAsync(request.Email, ct);

			if (user == null)
				throw new NotFoundException("Пользователь не зарегистрирован");

			if (!_passwordHashService.Validate(request.Password, user.PasswordHash!))
				throw new UnauthorizedException("Неверный логин или пароль");

			var responseUser = _mapper.Map<UserResponse>(user);

			return new LoginCommandResponse(responseUser);
		}
	}
}