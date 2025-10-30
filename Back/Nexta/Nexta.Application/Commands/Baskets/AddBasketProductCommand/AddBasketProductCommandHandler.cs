using Nexta.Application.DTO.Baskets;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Exceptions;
using AutoMapper;
using MediatR;
using Nexta.Domain.Models.Baskets;
using Nexta.Application.DTO.Products;

namespace Nexta.Application.Commands.Baskets.AddBasketProductCommand
{
	public class AddBasketProductCommandHandler : IRequestHandler<AddBasketProductCommand, BasketItemDto>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public AddBasketProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<BasketItemDto> Handle(AddBasketProductCommand command, CancellationToken ct)
		{
			var spec = new BasketByUserIdSpecification(command.UserId);
            var basket = await _unitOfWork.Baskets.GetByUserIdAsync(spec, ct);

			if (basket == null)
			{
				basket = new Basket(command.UserId);
				basket = await _unitOfWork.Baskets.AddAsync(basket, ct);
			}

			if (basket.Products.Select(p => p.ProductId).Contains(command.ProductId))
				throw new ConflictException("Деталь уже в корзине");

			var basketItem = basket.AddProduct(command.ProductId, command.Count);
			await _unitOfWork.SaveChangesAsync(ct);

			var product = await _unitOfWork.Products.GetByIdAsync(basketItem.ProductId, ct);

			var productDto = _mapper.Map<ProductDto>(product);

			var response = _mapper.Map<BasketItemDto>(basketItem) with { Product = productDto };

			return response;
        }
	}
}