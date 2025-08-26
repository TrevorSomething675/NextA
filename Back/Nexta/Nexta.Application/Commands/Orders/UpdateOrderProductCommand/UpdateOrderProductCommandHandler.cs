using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Orders.UpdateOrderProductCommand
{
	public class UpdateOrderProductCommandHandler : IRequestHandler<UpdateOrderProductCommand, UpdateOrderProductCommandResponse>
	{
		private readonly IOrderProductRepository _orderProductRepository;
		private readonly IMapper _mapper;

		public UpdateOrderProductCommandHandler(IOrderProductRepository orderProductRepository, IMapper mapper) 
		{
			_orderProductRepository = orderProductRepository;
			_mapper = mapper;
		}

		public async Task<UpdateOrderProductCommandResponse> Handle(UpdateOrderProductCommand command, CancellationToken ct = default)
		{
			var orderProduct = _mapper.Map<OrderProduct>(command);
			var updatedOrderProduct = await _orderProductRepository.UpdateAsync(orderProduct, ct);
			var result = _mapper.Map<OrderProductResponse>(updatedOrderProduct);

			return new UpdateOrderProductCommandResponse(result);
		}
	}
}