using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using MediatR;
using AutoMapper;
using Nexta.Domain.Models;

namespace Nexta.Application.Commands.DeleteDetailFromBasket
{
	public class DeleteBasketDetailCommandHandler : IRequestHandler<DeleteBasketDetailCommandRequest, Result<DeleteBasketDetailCommandResponse>>
	{
		private readonly IMapper _mapper;
		private readonly IUserDetailRepository _userDetailRepository;
		public DeleteBasketDetailCommandHandler(IUserDetailRepository userDetailRepository, IMapper mapper)
		{
			_userDetailRepository = userDetailRepository;
			_mapper = mapper;
		}
		public async Task<Result<DeleteBasketDetailCommandResponse>> Handle(DeleteBasketDetailCommandRequest request, CancellationToken ct)
		{
			try
			{
				var userDetail = await _userDetailRepository.GetAsync(request.UserId, request.DetailId, ct);
				if (userDetail == null)
					return new Result<DeleteBasketDetailCommandResponse>().NotFound();

				var userDetailToDelete = new UserDetailEntity
				{
					UserId = request.UserId,
					DetailId = request.DetailId
				};

				var deletedUserDetail = _mapper.Map<UserDetail>(await _userDetailRepository.DeleteAsync(userDetailToDelete, ct));

				return new Result<DeleteBasketDetailCommandResponse>(new DeleteBasketDetailCommandResponse(deletedUserDetail)).Success();
			}
			catch(Exception ex)
			{
				return new Result<DeleteBasketDetailCommandResponse>().BadRequest(ex.Message);
			}
		}
	}
}