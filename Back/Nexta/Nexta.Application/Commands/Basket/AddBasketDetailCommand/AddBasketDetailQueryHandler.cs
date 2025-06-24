using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Basket.AddBasketDetailCommand
{
	public class AddBasketDetailQueryHandler : IRequestHandler<AddBasketDetailQueryRequest, AddBasketDetailQueryResponse>
	{
		private readonly IUserDetailRepository _userDetailRepository;
		private readonly IDetailRepository _detailRepository;
		private readonly IMapper _mapper;

		public AddBasketDetailQueryHandler(IUserDetailRepository userDetailRepository, IMapper mapper, IDetailRepository detailRepository)
		{
			_userDetailRepository = userDetailRepository;
			_detailRepository = detailRepository;
			_mapper = mapper;
		}
		public async Task<AddBasketDetailQueryResponse> Handle(AddBasketDetailQueryRequest request, CancellationToken ct)
		{
			var userDetail = await _userDetailRepository.GetAsync(request.UserId, request.DetailId, ct);

			if (userDetail != null)
				throw new ConflictException("Деталь уже в корзине");

			var userDetailToCreate = new UserDetailEntity
			{ 
				UserId = request.UserId,
				DetailId = request.DetailId,
				Count = request.CountToPay,
				Status = UserDetailStatus.AtWork,
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
			};

			var createdUserDetail = _mapper.Map<UserDetail>(await _userDetailRepository.AddAsync(userDetailToCreate, ct));

			if (createdUserDetail == null)
				throw new BadRequestException("Деталь не получилось добавить");

			var createdDetail = _mapper.Map<Detail>(await _detailRepository.GetAsync(createdUserDetail.DetailId, ct));

			return new AddBasketDetailQueryResponse(createdDetail);
		}
	}
}