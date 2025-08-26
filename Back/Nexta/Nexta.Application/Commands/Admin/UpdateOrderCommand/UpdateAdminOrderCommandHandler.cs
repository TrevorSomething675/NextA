using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateOrderCommand
{
	public class UpdateAdminOrderCommandHandler : IRequestHandler<UpdateAdminOrderCommand, UpdateAdminOrderCommandResponse>
	{
		private readonly IOrderProductRepository _orderProductRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public UpdateAdminOrderCommandHandler(IOrderRepository orderRepository, IOrderProductRepository orderProductRepository, IMapper mapper)
		{
			_orderProductRepository = orderProductRepository;
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<UpdateAdminOrderCommandResponse> Handle(UpdateAdminOrderCommand request, CancellationToken ct = default)
		{
			var orderToUpdate = _mapper.Map<Order>(request);

			var updatedOrderId = await _orderRepository.UpdateAsync(orderToUpdate);

			if(orderToUpdate.OrderProducts != null)
				await _orderProductRepository.ReplaceOrderProductAsync(updatedOrderId, orderToUpdate.OrderProducts);

			return new UpdateAdminOrderCommandResponse(updatedOrderId);
		}
	}
}