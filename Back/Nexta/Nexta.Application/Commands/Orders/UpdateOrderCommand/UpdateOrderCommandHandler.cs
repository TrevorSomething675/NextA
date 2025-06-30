using MediatR;

namespace Nexta.Application.Commands.Orders.UpdateOrderCommand
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest, UpdateOrderCommandResponse>
	{
		public Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}