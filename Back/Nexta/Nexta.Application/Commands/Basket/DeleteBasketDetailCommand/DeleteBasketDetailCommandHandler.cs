using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Basket.DeleteBasketDetailCommand
{
	public class DeleteBasketDetailCommandHandler : IRequestHandler<DeleteBasketDetailCommandRequest, DeleteBasketDetailCommandResponse>
	{
		private readonly IUserDetailRepository _userDetailRepository;
		private readonly IMapper _mapper;
		public DeleteBasketDetailCommandHandler(IUserDetailRepository userDetailRepository, IMapper mapper)
		{
			_userDetailRepository = userDetailRepository;
			_mapper = mapper;
		}
		public async Task<DeleteBasketDetailCommandResponse> Handle(DeleteBasketDetailCommandRequest request, CancellationToken ct)
		{
			var userDetail = await _userDetailRepository.GetAsync(request.UserId, request.DetailId, ct);
			if (userDetail == null)
				throw new NotFoundException("В корзине нет такой детали");

			var deletedUserDetail = _mapper.Map<UserDetail>(await _userDetailRepository.DeleteAsync(userDetail, ct));

			return new DeleteBasketDetailCommandResponse(deletedUserDetail.UserId, deletedUserDetail.DetailId);
		}
	}
}