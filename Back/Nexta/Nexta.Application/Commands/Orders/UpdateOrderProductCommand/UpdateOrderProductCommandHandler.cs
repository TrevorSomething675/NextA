using Nexta.Application.DTO.Order;
using Nexta.Domain.Abstractions;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Orders.UpdateOrderProductCommand
{
	public class UpdateOrderProductCommandHandler : IRequestHandler<UpdateOrderProductCommand, OrderItemDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateOrderProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) 
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<OrderItemDto> Handle(UpdateOrderProductCommand command, CancellationToken ct = default)
		{
			var order = await _unitOfWork.Orders.GetAsync(command.OrderId, ct);
			order.UpdateProduct(command.ProductId, command.Count);
			await _unitOfWork.SaveChangesAsync(ct);

			var productItem = _unitOfWork.Products.GetAsync(command.ProductId, ct);
			var response = _mapper.Map<OrderItemDto>(productItem);

			return response;
        }
	}
}