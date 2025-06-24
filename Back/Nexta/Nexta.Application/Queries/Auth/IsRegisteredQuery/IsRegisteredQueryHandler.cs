using Nexta.Domain.Abstractions.Repositories;
using MediatR;

namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
	public class IsRegisteredQueryHandler : IRequestHandler<IsRegisteredQueryRequest, IsRegisteredQueryResponse>
	{
		private readonly IUserRepository _userRepository;

		public IsRegisteredQueryHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<IsRegisteredQueryResponse> Handle(IsRegisteredQueryRequest request, CancellationToken ct = default)
		{
			var user = await _userRepository.GetByEmailAsync(request.Email, ct);

			if (user != null)
				return new IsRegisteredQueryResponse(user != null);
			else
				return new IsRegisteredQueryResponse(false);
		}
	}
}