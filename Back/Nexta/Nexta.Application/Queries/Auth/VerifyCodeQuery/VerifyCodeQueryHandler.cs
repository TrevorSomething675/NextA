using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using Nexta.Application.DTO;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Auth.VerifyCodeQuery
{
    public class VerifyCodeQueryHandler : IRequestHandler<VerifyCodeQueryRequest, VerifyCodeQueryResponse>
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IValidator<VerifyCodeQueryRequest> _validator;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
		public VerifyCodeQueryHandler(IVerificationCodeService verificationCodeService, IMapper mapper,
			IValidator<VerifyCodeQueryRequest> validator, IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _verificationCodeService = verificationCodeService;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
            _validator = validator;
            _mapper = mapper;
        }

		public async Task<VerifyCodeQueryResponse> Handle(VerifyCodeQueryRequest request, CancellationToken ct = default)
		{
            var validationResult = await _validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

			var verifyResult = _verificationCodeService.VerifyCode(request.Email, request.Code);
            if (!verifyResult)
                throw new BadRequestException("Неверный код");

            var user = _mapper.Map<UserResponse>(await _userRepository.GetByEmailAsync(request.Email, ct));
            if (user == null)
                throw new BadRequestException("Неверный пользователь");

            var accessToken = _jwtTokenService.CreateAccessToken(user.Email!, user.Role.ToString());

            return new VerifyCodeQueryResponse(user, accessToken);
        }
	}
}