using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateOrderDetailCommand
{
	public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommandRequest, UpdateOrderDetailCommandResponse>
	{
		private readonly IOrderDetailRepository _orderDetailRepository;
		private readonly IMapper _mapper;

		public UpdateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository, IMapper mapper) 
		{
			_orderDetailRepository = orderDetailRepository;
			_mapper = mapper;
		}

		public async Task<UpdateOrderDetailCommandResponse> Handle(UpdateOrderDetailCommandRequest request, CancellationToken ct = default)
		{
			var orderDetail = _mapper.Map<OrderDetail>(request);
			var updatedOrderDetail = await _orderDetailRepository.UpdateAsync(orderDetail, ct);
			var response = _mapper.Map<OrderDetailResponse>(updatedOrderDetail);

			return new UpdateOrderDetailCommandResponse(response);
		}
	}
}