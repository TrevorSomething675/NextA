using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
using FluentValidation;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Auth.RegisterCommand
{
	public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
		private readonly IHashService _passwordHashService;
		private readonly IValidator<RegisterCommand> _validator;
        private readonly IVerificationCodeService _verificationCodeService;
        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper, 
			IHashService passwordHashService, IValidator<RegisterCommand> validator, 
			IJwtTokenService jwtTokenService, IVerificationCodeService verificationCodeService)
		{
			_verificationCodeService = verificationCodeService;
			_passwordHashService = passwordHashService;
			_jwtTokenService = jwtTokenService;
			_userRepository = userRepository;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<RegisterCommandResponse> Handle(RegisterCommand command, CancellationToken ct)
		{
			var validationResult = await _validator.ValidateAsync(command, ct);

			if (!validationResult.IsValid)
				throw new BadRequestException(string.Join(", ", validationResult.Errors));

			var verifyResult = _verificationCodeService.VerifyCode(command.Email, command.Code);
            if (!verifyResult)
                throw new BadRequestException("Неверный код");

            var dbUser = await _userRepository.GetByEmailAsync(command.Email, ct);
			if (dbUser != null)
				throw new ConflictException("Такой пользователь уже существует");

			var passwordHash = _passwordHashService.Generate(command.Password);
			var user = _mapper.Map<User>(command);

			var userToCreate = CreateUserToRegister(user, passwordHash!);

			var createdUser = _mapper.Map<UserResponse>(await _userRepository.AddAsync(userToCreate, ct));

			var accessToken = _jwtTokenService.CreateAccessToken(createdUser.Email!, createdUser.Role);

			return new RegisterCommandResponse(createdUser, accessToken);
		}

		private User CreateUserToRegister(User user, string passwordHash)
		{
			var userToCreate = new User
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				MiddleName = user.MiddleName,
				Email = user.Email,
				Phone = user.Phone,
				PasswordHash = passwordHash,
				Role = "User"
			};

			return userToCreate;
		}
	}
}