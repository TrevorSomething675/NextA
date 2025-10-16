using Nexta.Domain.Abstractions.Repositories;
using AutoMapper;
using MediatR;
using Nexta.Domain.Models.User;

namespace Nexta.Application.Commands.Account.UpdateAccountCommand
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, UpdateAccountCommandResponse>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<UpdateAccountCommandResponse> Handle(UpdateAccountCommand command, CancellationToken ct)
        {
            var userToUpdate = _mapper.Map<User>(command);

            var updatedUser = await _usersRepository.UpdateAsync(userToUpdate, ct);

            return new UpdateAccountCommandResponse(updatedUser);
        }
    }
}