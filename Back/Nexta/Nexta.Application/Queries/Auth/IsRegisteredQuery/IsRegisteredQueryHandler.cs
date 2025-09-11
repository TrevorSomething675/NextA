using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using MediatR;

namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
	public class IsRegisteredQueryHandler : IRequestHandler<IsRegisteredQuery, Unit>
	{
		private readonly IUserRepository _userRepository;

		public IsRegisteredQueryHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<Unit> Handle(IsRegisteredQuery query, CancellationToken ct = default)
		{
			var user = await _userRepository.GetByEmailAsync(query.Email, ct);
			if (user == null)
				throw new NotFoundException("Пользователь не найден");

			return Unit.Value;
		}
	}
}