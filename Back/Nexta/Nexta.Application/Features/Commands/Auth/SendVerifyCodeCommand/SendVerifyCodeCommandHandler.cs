using FluentValidation;
using MediatR;
using Nexta.Application.Abstractions;
using Nexta.Application.Interfaces;

namespace Nexta.Application.Commands.Auth.SendVerifyCodeCommand
{
    public class SendVerifyCodeCommandHandler : IRequestHandler<SendVerifyCodeCommand, Unit>
    {
        private readonly IVerificationCodeService _verificationService;
        private readonly IValidator<SendVerifyCodeCommand> _validator;
        private readonly IEmailService _emailService;

        public SendVerifyCodeCommandHandler(IVerificationCodeService verificationService,
            IEmailService emailService, IValidator<SendVerifyCodeCommand> validator)
        {
            _verificationService = verificationService;
            _emailService = emailService;
            _validator = validator;
        }

        public async Task<Unit> Handle(SendVerifyCodeCommand command, CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);
            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

            var code = _verificationService.SetVerificationCode(command.Email);
            var subject = CreateSubject(code);

            await _emailService.SendEmailAsync(command.Email, "", subject, "", ct);

            return Unit.Value;
        }

        private string CreateSubject(string code)
        {
            return string.Format("Код подтверждения: {0}", code);
        }
    }
}