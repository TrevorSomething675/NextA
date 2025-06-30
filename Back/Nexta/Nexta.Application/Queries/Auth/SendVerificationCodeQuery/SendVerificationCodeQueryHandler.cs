using Nexta.Domain.Abstractions.Services;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Queries.Auth.SendVerificationCodeQuery
{
    public class SendVerificationCodeQueryHandler : IRequestHandler<SendVerificationCodeQueryRequest, Unit>
    {
        private readonly IVerificationCodeService _verificationService;
        private readonly IEmailService _emailService;
        private readonly IValidator<SendVerificationCodeQueryRequest> _validator;
		public SendVerificationCodeQueryHandler(IVerificationCodeService verificationService, 
            IEmailService emailService, IValidator<SendVerificationCodeQueryRequest> validator)
        {
            _verificationService = verificationService;
            _emailService = emailService;
            _validator = validator;
        }

		public async Task<Unit> Handle(SendVerificationCodeQueryRequest request, CancellationToken ct = default)
		{
            var validationResult = await _validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

            var result = _verificationService.SetVerificationCode(request.Email);
            var subject = CreateSubject(result.Code);

			await _emailService.SendEmailAsync(request.Email, "", subject, "", ct);

            return Unit.Value;
		}

        private string CreateSubject(string code)
        {
            return string.Format("Код подтверждения: {0}", code); 
        }
	}
}