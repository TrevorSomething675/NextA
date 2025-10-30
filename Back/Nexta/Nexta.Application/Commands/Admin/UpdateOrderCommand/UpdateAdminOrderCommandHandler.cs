using Nexta.Domain.Abstractions;
using Nexta.Domain.Models.Orders;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateOrderCommand
{
	public class UpdateAdminOrderCommandHandler : IRequestHandler<UpdateAdminOrderCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateAdminOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Guid> Handle(UpdateAdminOrderCommand request, CancellationToken ct = default)
		{
			var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);

			order.UpdateStatus(request.Status);
			order.ReplaceProducts(_mapper.Map<List<OrderItem>>(request.OrderProducts));

			var result = _unitOfWork.Orders.Update(order);
			await _unitOfWork.SaveChangesAsync(ct);

			return result.Id;
		}
	}
}