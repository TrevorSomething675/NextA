using Nexta.Domain.Abstractions;
using Nexta.Domain.Models.Order;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateOrderCommand
{
	public class UpdateAdminOrderCommandHandler : IRequestHandler<UpdateAdminOrderCommand, UpdateAdminOrderCommandResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateAdminOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<UpdateAdminOrderCommandResponse> Handle(UpdateAdminOrderCommand request, CancellationToken ct = default)
		{
			var order = await _unitOfWork.Orders.GetAsyncByUserId(request.UserId);

			order.UpdateStatus(request.Status);
			order.ReplaceProducts(_mapper.Map<List<OrderItem>>(request.OrderProducts));

			var result = _unitOfWork.Orders.Update(order);
			await _unitOfWork.SaveChangesAsync(ct);

			return new UpdateAdminOrderCommandResponse(result.Id);
		}
	}
}