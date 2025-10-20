using Nexta.Application.DTO.Product;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Basket.AddBasketProductCommand
{
	public class AddBasketProductCommandHandler : IRequestHandler<AddBasketProductCommand, ProductDto>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public AddBasketProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<ProductDto> Handle(AddBasketProductCommand command, CancellationToken ct)
		{
			var spec = new BasketByUserIdSpecification(command.UserId);
            var basket = await _unitOfWork.Baskets.GetByUserIdAsync(spec, ct);

			if (basket.Products.Select(p => p.Id).Contains(command.ProductId))
				throw new ConflictException("Деталь уже в корзине");

			basket.AddProduct(command.ProductId, command.CountToPay);
			await _unitOfWork.SaveChangesAsync(ct);

			var product = _unitOfWork.Products.GetAsync(command.ProductId, ct);
			var response = _mapper.Map<ProductDto>(product);

			return response;
        }
	}
}