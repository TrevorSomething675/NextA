using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Commands.Account.UpdateEmailCommand
{
    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, UpdateEmailCommandResponse>
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IValidator<UpdateEmailCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmailCommandHandler(IUnitOfWork unitOfWork, 
            IVerificationCodeService verificationCodeService, IValidator<UpdateEmailCommand> validator)
        {
            _verificationCodeService = verificationCodeService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<UpdateEmailCommandResponse> Handle(UpdateEmailCommand command, CancellationToken ct)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(',', validationResult.Errors));

            var verifyResult = _verificationCodeService.VerifyCode(command.Email, command.Code);
            if (!verifyResult)
                throw new BadRequestException("Неверный код");

            var dbUser = await _unitOfWork.Users.GetByEmailAsync(command.LegacyEmail, ct);
            if (dbUser == null)
                throw new NotFoundException("Пользователь не зарегистрирован");

            if(dbUser.Email != command.LegacyEmail)
                throw new NotFoundException("Ошибка валидации"); // сделано специально

            dbUser.ChangeEmail(command.Email);

            var updateUser = _unitOfWork.Users.Update(dbUser);

            return new UpdateEmailCommandResponse(updateUser);
        }
    }
}
