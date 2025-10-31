using Nexta.Application.DTO.Users;
using Nexta.Domain.Exceptions;
using FluentValidation;
using AutoMapper;
using MediatR;
using Nexta.Application.Interfaces;
using Nexta.Application.Abstractions;

namespace Nexta.Application.Commands.Auth.LoginCommand
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand, UserDto>
	{
		private readonly IValidator<LoginCommand> _validator;
		private readonly IHashService _passwordHashService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public LoginCommandHandler(IHashService passwordHashService, IUnitOfWork unitOfWork,
			IMapper mapper, IValidator<LoginCommand> validator)
		{
			_passwordHashService = passwordHashService;
			_unitOfWork = unitOfWork;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<UserDto> Handle(LoginCommand command, CancellationToken ct = default)
		{
			var validationResult = await _validator.ValidateAsync(command, ct);
			if (!validationResult.IsValid)
				throw new BadRequestException(string.Join(", ", validationResult.Errors));

			var user = await _unitOfWork.Users.GetByEmailAsync(command.Email, ct);

			if (user == null)
				throw new NotFoundException("Пользователь не зарегистрирован");

			if (!_passwordHashService.Validate(command.Password, user.PasswordHash!))
				throw new UnauthorizedException("Неверный логин или пароль");

			var response = _mapper.Map<UserDto>(user);

			return response;

        }
	}
}