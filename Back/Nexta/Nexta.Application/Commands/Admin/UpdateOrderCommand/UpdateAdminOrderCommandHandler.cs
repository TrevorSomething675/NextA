using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateOrderCommand
{
	public class UpdateAdminOrderCommandHandler : IRequestHandler<UpdateAdminOrderCommandRequest, UpdateAdminOrderCommandResponse>
	{
		private readonly IOrderDetailRepository _orderDetailRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public UpdateAdminOrderCommandHandler(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IMapper mapper)
		{
			_orderDetailRepository = orderDetailRepository;
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<UpdateAdminOrderCommandResponse> Handle(UpdateAdminOrderCommandRequest request, CancellationToken ct = default)
		{
			var orderToUpdate = _mapper.Map<Order>(request);

			var updatedOrderId = await _orderRepository.UpdateAsync(orderToUpdate);

			if(orderToUpdate.OrderDetails != null)
				await _orderDetailRepository.ReplaceOrderDetailAsync(updatedOrderId, orderToUpdate.OrderDetails);

			return new UpdateAdminOrderCommandResponse(updatedOrderId);
		}
	}
}