using Nexta.Domain.Abstractions.Services;
using Nexta.Application.DTO.User;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Exceptions;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Auth.VerifyCodeCommand
{
    public class VerifyCodeCommandHandler : IRequestHandler<VerifyCodeCommand, VerifyCodeCommandResponse>
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IValidator<VerifyCodeCommand> _validator;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

		public VerifyCodeCommandHandler(IVerificationCodeService verificationCodeService, IMapper mapper,
			IValidator<VerifyCodeCommand> validator, IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
        {
            _verificationCodeService = verificationCodeService;
            _jwtTokenService = jwtTokenService;
            _unitOfWork = unitOfWork;
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

            var user = _mapper.Map<UserDto>(await _unitOfWork.Users.GetByEmailAsync(command.Email, ct));
            if (user == null)
                throw new BadRequestException("Неверный пользователь");
            
            var accessToken = _jwtTokenService.CreateAccessToken(user.Email!, user.Role);
            var response = _mapper.Map<VerifyCodeCommandResponse>(user) with { AccessToken = accessToken };

            return response;
        }
	}
}