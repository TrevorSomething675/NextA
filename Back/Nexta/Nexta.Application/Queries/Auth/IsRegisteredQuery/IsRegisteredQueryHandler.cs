using Nexta.Domain.Abstractions.Repositories;
using MediatR;

namespace Nexta.Application.Queries.AuthQueries.CheckRegisterUserQuery
{
	public class CheckRegisterUserQueryHandler : IRequestHandler<CheckRegisterUserQueryRequest, CheckRegisterUserQueryResponse>
	{
		private readonly IUserRepository _userRepository;

		public CheckRegisterUserQueryHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<CheckRegisterUserQueryResponse> Handle(CheckRegisterUserQueryRequest request, CancellationToken ct = default)
		{
			var user = await _userRepository.GetByEmailAsync(request.Email, ct);

			if (user != null)
				return new CheckRegisterUserQueryResponse(user != null);
			else
				return new CheckRegisterUserQueryResponse(false);
		}
	}
}