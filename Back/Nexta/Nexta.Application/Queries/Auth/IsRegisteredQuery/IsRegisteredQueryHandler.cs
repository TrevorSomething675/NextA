using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using MediatR;

namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
	public class IsRegisteredQueryHandler : IRequestHandler<IsRegisteredQuery, Unit>
	{
		private readonly IUsersRepository _usersRepository;

		public IsRegisteredQueryHandler(IUsersRepository usersRepository)
		{
			_usersRepository = usersRepository;
		}

		public async Task<Unit> Handle(IsRegisteredQuery query, CancellationToken ct = default)
		{
			var user = await _usersRepository.GetByEmailAsync(query.Email, ct);
			if (user == null)
				throw new NotFoundException("Пользователь не найден");

			return Unit.Value;
		}
	}
}