using Nexta.Application.DTO.Baskets;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using MediatR;

namespace Nexta.Application.Commands.Baskets.UpdateBasketProductCommand
{
	public class UpdateBasketProductCommandHandler : IRequestHandler<UpdateBasketProductCommand, BasketItemDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public UpdateBasketProductCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<BasketItemDto> Handle(UpdateBasketProductCommand command, CancellationToken ct)
		{
			var spec = new BasketByUserIdSpecification(command.UserId);

			var basket = await _unitOfWork.Baskets.GetByUserIdAsync(spec, ct);
			basket.UpdateProduct(command.ProductId, command.Count);
			await _unitOfWork.SaveChangesAsync(ct);

			var response = new BasketItemDto(command.ProductId, command.Count);

			return response;
		}
	}
}
