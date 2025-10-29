using Nexta.Domain.Abstractions;
using Nexta.Domain.Models.User;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Account.UpdateAccountCommand
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, UpdateAccountCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateAccountCommandResponse> Handle(UpdateAccountCommand command, CancellationToken ct)
        {
            var userToUpdate = _mapper.Map<User>(command);

            var updatedUser = _unitOfWork.Users.Update(userToUpdate);
            await _unitOfWork.SaveChangesAsync(ct);

            return new UpdateAccountCommandResponse(updatedUser);
        }
    }
}