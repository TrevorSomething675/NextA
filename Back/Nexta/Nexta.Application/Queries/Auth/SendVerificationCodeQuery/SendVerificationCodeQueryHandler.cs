using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using MediatR;
using FluentValidation;

namespace Nexta.Application.Queries.CodeQueries.SendVerificationCodeQuery
{
    public class SendVerificationCodeQueryHandler : IRequestHandler<SendVerificationCodeQueryRequest, SendVerificationCodeQueryResponse>
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

		public async Task<SendVerificationCodeQueryResponse> Handle(SendVerificationCodeQueryRequest request, CancellationToken ct = default)
		{
            var validationResult = await _validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

            var result = _verificationService.SetVerificationCode(request.Email);
            var subject = CreateSubject(result.Code);

			await _emailService.SendEmailAsync(request.Email, "", subject, "", ct);

            //if (!result)
            //    throw new BadRequestException("Не удалось отправить код");

            return new SendVerificationCodeQueryResponse(true);
		}

        private string CreateSubject(string code)
        {
            return string.Format("Код подтверждения: {0}", code); 
        }
	}
}