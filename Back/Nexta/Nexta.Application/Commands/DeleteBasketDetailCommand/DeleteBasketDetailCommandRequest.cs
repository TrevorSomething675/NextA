using MediatR;
using Nexta.Domain.Models.DataModels;

namespace Nexta.Application.Commands.DeleteDetailFromBasket
{
    public class DeleteBasketDetailCommandRequest : IRequest<Result<DeleteBasketDetailCommandResponse>>
	{
		public Guid UserId { get; set; }
		public Guid DetailId { get; set; }
	}
}
