using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using MediatR;
using AutoMapper;
using Nexta.Domain.Models;
using Nexta.Domain.Exceptions;

namespace Nexta.Application.Commands.DeleteDetailFromBasket
{
	public class DeleteBasketDetailCommandHandler : IRequestHandler<DeleteBasketDetailCommandRequest, DeleteBasketDetailCommandResponse>
	{
		private readonly IMapper _mapper;
		private readonly IUserDetailRepository _userDetailRepository;
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

			var userDetailToDelete = new UserDetailEntity
			{
				UserId = request.UserId,
				DetailId = request.DetailId
			};

			var deletedUserDetail = _mapper.Map<UserDetail>(await _userDetailRepository.DeleteAsync(userDetailToDelete, ct));

			return new DeleteBasketDetailCommandResponse(deletedUserDetail);
		}
	}
}