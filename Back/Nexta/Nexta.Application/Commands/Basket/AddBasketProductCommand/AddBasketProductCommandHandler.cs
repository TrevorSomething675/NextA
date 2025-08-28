using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;

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
		public async Task<AddBasketProductCommandResponse> Handle(AddBasketProductCommand command, CancellationToken ct)
		{
			var basketProduct = await _basketProductRepository.GetAsync(command.UserId, command.ProductId, ct);

			if (basketProduct != null)
				throw new ConflictException("Деталь уже в корзине");

			var basketProductToCreate = new BasketProduct
			{ 
				UserId = command.UserId,
				ProductId = command.ProductId,
				Count = command.CountToPay,
				Status = BasketProductStatus.AtWork,
			};

			var createdBasketProduct = _mapper.Map<BasketProduct>(await _basketProductRepository.AddAsync(basketProductToCreate, ct));

			if (createdBasketProduct == null)
				throw new BadRequestException("Деталь не получилось добавить");

			var basketResponse = _mapper.Map<BasketProductResponse>(createdBasketProduct);

			return new AddBasketProductCommandResponse(basketResponse);
		}
	}
}