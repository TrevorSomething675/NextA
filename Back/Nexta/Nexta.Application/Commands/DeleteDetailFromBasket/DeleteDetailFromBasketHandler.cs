using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using MediatR;
using AutoMapper;
using Nexta.Domain.Models;

namespace Nexta.Application.Commands.DeleteDetailFromBasket
{
	public class DeleteDetailFromBasketHandler : IRequestHandler<DeleteDetailFromBasketRequest, Result<DeleteDetailFromBasketResponse>>
	{
		private readonly IMapper _mapper;
		private readonly IUserDetailRepository _userDetailRepository;
		public DeleteDetailFromBasketHandler(IUserDetailRepository userDetailRepository, IMapper mapper)
		{
			_userDetailRepository = userDetailRepository;
			_mapper = mapper;
		}
		public async Task<Result<DeleteDetailFromBasketResponse>> Handle(DeleteDetailFromBasketRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var userDetail = await _userDetailRepository.Get(request.UserId, request.DetailId);
				if (userDetail == null)
					return new Result<DeleteDetailFromBasketResponse>().NotFound();

				var userDetailToDelete = new UserDetailEntity
				{
					UserId = request.UserId,
					DetailId = request.DetailId
				};

				var deletedUserDetail = _mapper.Map<UserDetail>(await _userDetailRepository.Delete(userDetailToDelete));

				return new Result<DeleteDetailFromBasketResponse>().Success();
			}
			catch(Exception ex)
			{
				return new Result<DeleteDetailFromBasketResponse>().BadRequest(ex.Message);
			}
		}
	}
}