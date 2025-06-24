using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Queries.Auth.VerifyCodeQuery
{
    public class VerifyCodeQueryHandler : IRequestHandler<VerifyCodeQueryRequest, VerifyCodeQueryResponse>
    {
        private readonly IVerificationCodeService _verificationCodeService;
		private readonly IJwtTokenService _jwtTokenService;
        private readonly IValidator<VerifyCodeQueryRequest> _validator;
		public VerifyCodeQueryHandler(IVerificationCodeService verificationCodeService, IJwtTokenService jwtTokenService,
			IValidator<VerifyCodeQueryRequest> validator) 
        {
            _verificationCodeService = verificationCodeService;
            _jwtTokenService = jwtTokenService;
            _validator = validator;
        }

		public async Task<VerifyCodeQueryResponse> Handle(VerifyCodeQueryRequest request, CancellationToken ct = default)
		{
            var validationResult = await _validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

			var verifyResult = _verificationCodeService.VerifyCode(request.Email, request.Code);
            if (!verifyResult)
                throw new BadRequestException("Неверный код");

            var accessToken = _jwtTokenService.CreateAccessToken(request.UserId, request.Role);
            if (accessToken == null)
                throw new BadRequestException("Не удалось авторизоваться");

            return new VerifyCodeQueryResponse(accessToken);
        }
	}
}
