using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using MediatR;

namespace Nexta.Application.Queries.Auth.IsRegistrationQuery
{
	public class IsRegistrationQueryHandler : IRequestHandler<IsRegistrationQuery, Unit>
	{
		private readonly IUsersRepository _usersRepository;

		public IsRegistrationQueryHandler(IUsersRepository usersRepository)
		{
			_usersRepository = usersRepository;
		}

		public async Task<Unit> Handle(IsRegistrationQuery query, CancellationToken ct = default)
		{
			var user = await _usersRepository.GetByEmailAsync(query.Email, ct);
			if (user == null)
				throw new NotFoundException("Пользователь не найден");

			return Unit.Value;
		}
	}
}