using Nexta.Domain.Abstractions.Repositories;
using MediatR;

namespace Nexta.Application.Commands.Orders.DeleteOrderCommand
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, DeleteOrderCommandResponse>
	{
		private readonly IOrderRepository _orderRepository;

		public DeleteOrderCommandHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
		{
			var orderId = await _orderRepository.DeleteAsync(request.OrderId);

			return new DeleteOrderCommandResponse(orderId);
		}
	}
}