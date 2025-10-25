using Nexta.Domain.Abstractions.Repositories;
using MediatR;

namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
	public class IsRegisteredQueryHandler : IRequestHandler<IsRegisteredQuery, IsRegisteredQueryResponse>
	{
		private readonly IUsersRepository _usersRepository;

		public IsRegisteredQueryHandler(IUsersRepository usersRepository)
		{
			_usersRepository = usersRepository;
		}

		public async Task<IsRegisteredQueryResponse> Handle(IsRegisteredQuery query, CancellationToken ct = default)
		{
			var user = await _usersRepository.GetByEmailAsync(query.Email, ct);
			if (user == null)
				return new IsRegisteredQueryResponse(false);

			return new IsRegisteredQueryResponse(true);
		}
	}
}