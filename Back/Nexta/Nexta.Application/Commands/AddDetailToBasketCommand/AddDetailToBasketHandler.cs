using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.AddDetailToBasketCommand
{
	public class AddDetailToBasketHandler : IRequestHandler<AddDetailToBasketRequest, Result<AddDetailToBasketResponse>>
	{
		private readonly IUserDetailRepository _userDetailRepository;
		private readonly IMapper _mapper;

		public AddDetailToBasketHandler(IUserDetailRepository userDetailRepository, IMapper mapper)
		{
			_userDetailRepository = userDetailRepository;
			_mapper = mapper;
		}
		public async Task<Result<AddDetailToBasketResponse>> Handle(AddDetailToBasketRequest request, CancellationToken ct)
		{
			try
			{
				var userDetail = await _userDetailRepository.Get(request.UserId, request.DetailId, ct);

				if (userDetail != null)
					return new Result<AddDetailToBasketResponse>().BadRequest("Деталь уже в корзине");

				var userDetailToCreate = new UserDetailEntity 
				{ 
					UserId = request.UserId, 
					DetailId = request.DetailId 
				};

				var createdUserDetail = _mapper.Map<UserDetail>(await _userDetailRepository.Add(userDetailToCreate));

				return new Result<AddDetailToBasketResponse>(new AddDetailToBasketResponse(createdUserDetail)).Success();
			}
			catch (Exception ex)
			{
				return new Result<AddDetailToBasketResponse>().Invalid(ex.Message);
			}
		}
	}
}