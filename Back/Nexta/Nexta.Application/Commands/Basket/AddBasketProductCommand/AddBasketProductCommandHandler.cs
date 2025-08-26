using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Basket.AddBasketProductCommand
{
	public class AddBasketProductCommandHandler : IRequestHandler<AddBasketProductCommand, AddBasketProductCommandResponse>
	{
		private readonly IBasketProductRepository _basketProductRepository;
		private readonly IMapper _mapper;

		public AddBasketProductCommandHandler(IBasketProductRepository basketProductRepository, IMapper mapper)
		{
			_basketProductRepository = basketProductRepository;
			_mapper = mapper;
		}
		public async Task<AddBasketProductCommandResponse> Handle(AddBasketProductCommand request, CancellationToken ct)
		{
			var basketProduct = await _basketProductRepository.GetAsync(request.UserId, request.ProductId, ct);

			if (basketProduct != null)
				throw new ConflictException("Деталь уже в корзине");

			var basketProductToCreate = new BasketProduct
			{ 
				UserId = request.UserId,
				ProductId = request.ProductId,
				Count = request.CountToPay,
				Status = UserDetailStatus.AtWork,
			};

			var basketDetailResponse = _mapper.Map<BasketProduct>(await _basketProductRepository.AddAsync(basketProductToCreate, ct));

			if (basketDetailResponse == null)
				throw new BadRequestException("Деталь не получилось добавить");

			return new AddBasketProductCommandResponse(basketDetailResponse);
		}
	}
}