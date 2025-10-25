using Nexta.Domain.Abstractions.Services;
using Nexta.Application.DTO.User;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Models.User;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Constants;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Auth.RegisterCommand
{
	public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
	{
		private readonly IMapper _mapper;
		private readonly IEmailService _emailService;
        private readonly IJwtTokenService _jwtTokenService;
		private readonly IUnitOfWork _unitOfWork;
        private readonly IHashService _passwordHashService;
		private readonly IValidator<RegisterCommand> _validator;
        private readonly IVerificationCodeService _verificationCodeService;
        public RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, 
			IHashService passwordHashService, IValidator<RegisterCommand> validator, IEmailService emailService, 
			IJwtTokenService jwtTokenService, IVerificationCodeService verificationCodeService)
		{
			_verificationCodeService = verificationCodeService;
			_passwordHashService = passwordHashService;
			_jwtTokenService = jwtTokenService;
			_emailService = emailService;
			_unitOfWork = unitOfWork;
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

            var dbUser = await _unitOfWork.Users.GetByEmailAsync(command.Email, ct);
			if (dbUser != null)
				throw new ConflictException("Такой пользователь уже существует");

			var passwordHash = _passwordHashService.Generate(command.Password);

			var user = new User(command.FirstName, command.MiddleName, command.Email ,command.LastName, passwordHash);
			user.AddNotification("Успешная регистрация!", NotificationKeys.CompleteRegistration);
			var createdUser = _mapper.Map<UserDto>(await _unitOfWork.Users.AddAsync(user, ct));

			await _unitOfWork.SaveChangesAsync(ct);

			var accessToken = _jwtTokenService.CreateAccessToken(createdUser.Email!, createdUser.Role);

			await _emailService.SendEmailAsync(createdUser.Email, "", "Успешная регистрация!", NotificationKeys.CompleteRegistration, ct);
			var response = _mapper.Map<RegisterCommandResponse>(createdUser) with { AccessToken = accessToken };

			return response;
        }
	}
}