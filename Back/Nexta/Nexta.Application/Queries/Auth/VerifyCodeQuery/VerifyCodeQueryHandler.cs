using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Queries.Auth.VerifyCodeQuery
{
    public class VerifyCodeQueryHandler : IRequestHandler<VerifyCodeQueryRequest, Unit>
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IValidator<VerifyCodeQueryRequest> _validator;
		public VerifyCodeQueryHandler(IVerificationCodeService verificationCodeService,
			IValidator<VerifyCodeQueryRequest> validator) 
        {
            _verificationCodeService = verificationCodeService;
            _validator = validator;
        }

		public async Task<Unit> Handle(VerifyCodeQueryRequest request, CancellationToken ct = default)
		{
            var validationResult = await _validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

			var verifyResult = _verificationCodeService.VerifyCode(request.Email, request.Code);
            if (!verifyResult)
                throw new BadRequestException("Неверный код");

            return Unit.Value;
        }
	}
}