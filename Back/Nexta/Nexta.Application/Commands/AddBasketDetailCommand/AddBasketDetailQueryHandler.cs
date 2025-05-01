using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.AddDetailToBasketCommand
{
	public class AddBasketDetailQueryHandler : IRequestHandler<AddBasketDetailQueryRequest, Result<AddBasketDetailQueryResponse>>
	{
		private readonly IUserDetailRepository _userDetailRepository;
		private readonly IMapper _mapper;

		public AddBasketDetailQueryHandler(IUserDetailRepository userDetailRepository, IMapper mapper)
		{
			_userDetailRepository = userDetailRepository;
			_mapper = mapper;
		}
		public async Task<Result<AddBasketDetailQueryResponse>> Handle(AddBasketDetailQueryRequest request, CancellationToken ct)
		{
			try
			{
				var userDetail = await _userDetailRepository.Get(request.UserId, request.DetailId, ct);

				if (userDetail != null)
					return new Result<AddBasketDetailQueryResponse>().BadRequest("Деталь уже в корзине");

				var userDetailToCreate = new UserDetailEntity 
				{ 
					UserId = request.UserId, 
					DetailId = request.DetailId 
				};

				var createdUserDetail = _mapper.Map<UserDetail>(await _userDetailRepository.Add(userDetailToCreate));

				return new Result<AddBasketDetailQueryResponse>(new AddBasketDetailQueryResponse(createdUserDetail)).Success();
			}
			catch (Exception ex)
			{
				return new Result<AddBasketDetailQueryResponse>().Invalid(ex.Message);
			}
		}
	}
}