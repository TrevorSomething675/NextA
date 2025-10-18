using Nexta.Application.DTO.Basket;
using Nexta.Domain.Abstractions;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Basket.UpdateBasketProductCommand
{
	public class UpdateBasketProductCommandHandler : IRequestHandler<UpdateBasketProductCommand, BasketItemDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateBasketProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BasketItemDto> Handle(UpdateBasketProductCommand command, CancellationToken ct)
		{
			var basket = await _unitOfWork.Baskets.GetByUserIdAsync(command.UserId, ct);
			basket.UpdateProduct(command.ProductId, command.Count);
			await _unitOfWork.SaveChangesAsync(ct);

			var response = new BasketItemDto(command.ProductId, command.Count);

			return response;
		}
	}
}
