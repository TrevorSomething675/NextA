using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using FluentValidation;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Auth.VerifyCodeQuery
{
    public class VerifyCodeCommandHandler : IRequestHandler<VerifyCodeCommand, VerifyCodeCommandResponse>
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IValidator<VerifyCodeCommand> _validator;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

		public VerifyCodeCommandHandler(IVerificationCodeService verificationCodeService, IMapper mapper,
			IValidator<VerifyCodeCommand> validator, IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _verificationCodeService = verificationCodeService;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
            _validator = validator;
            _mapper = mapper;
        }

		public async Task<VerifyCodeCommandResponse> Handle(VerifyCodeCommand command, CancellationToken ct = default)
		{
            var validationResult = await _validator.ValidateAsync(command, ct);
            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

			var verifyResult = _verificationCodeService.VerifyCode(command.Email, command.Code);
            if (!verifyResult)
                throw new BadRequestException("Неверный код");

            var user = _mapper.Map<UserResponse>(await _userRepository.GetByEmailAsync(command.Email, ct));
            if (user == null)
                throw new BadRequestException("Неверный пользователь");
            
            var accessToken = _jwtTokenService.CreateAccessToken(user.Email!, user.Role);

            return new VerifyCodeCommandResponse(user, accessToken);
        }
	}
}