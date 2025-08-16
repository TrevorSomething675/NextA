using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Basket.AddBasketDetailCommand
{
	public class AddBasketDetailQueryHandler : IRequestHandler<AddBasketDetailQueryRequest, AddBasketDetailQueryResponse>
	{
		private readonly IUserDetailRepository _userDetailRepository;
		private readonly IMapper _mapper;

		public AddBasketDetailQueryHandler(IUserDetailRepository userDetailRepository, IMapper mapper)
		{
			_userDetailRepository = userDetailRepository;
			_mapper = mapper;
		}
		public async Task<AddBasketDetailQueryResponse> Handle(AddBasketDetailQueryRequest request, CancellationToken ct)
		{
			var userDetail = await _userDetailRepository.GetAsync(request.UserId, request.DetailId, ct);

			if (userDetail != null)
				throw new ConflictException("Деталь уже в корзине");

			var userDetailToCreate = new UserDetail
			{ 
				UserId = request.UserId,
				DetailId = request.DetailId,
				Count = request.CountToPay,
				Status = UserDetailStatus.AtWork,
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
			};

			var userDetailResponse = _mapper.Map<UserDetailResponse>(await _userDetailRepository.AddAsync(userDetailToCreate, ct));

			if (userDetailResponse == null)
				throw new BadRequestException("Деталь не получилось добавить");

			return new AddBasketDetailQueryResponse(userDetailResponse);
		}
	}
}