using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using MediatR;

namespace Nexta.Application.Commands.Basket.DeleteBasketDetailCommand
{
	public class DeleteBasketDetailCommandHandler : IRequestHandler<DeleteBasketDetailCommandRequest, DeleteBasketDetailCommandResponse>
	{
		private readonly IUserDetailRepository _userDetailRepository;

		public DeleteBasketDetailCommandHandler(IUserDetailRepository userDetailRepository)
		{
			_userDetailRepository = userDetailRepository;
		}

		public async Task<DeleteBasketDetailCommandResponse> Handle(DeleteBasketDetailCommandRequest request, CancellationToken ct)
		{
			var userDetail = await _userDetailRepository.GetAsync(request.UserId, request.DetailId, ct);
			if (userDetail == null)
				throw new NotFoundException("В корзине нет такой детали");

			var deletedUserDetail = await _userDetailRepository.DeleteAsync(userDetail, ct);

			return new DeleteBasketDetailCommandResponse(deletedUserDetail.UserId, deletedUserDetail.DetailId);
		}
	}
}