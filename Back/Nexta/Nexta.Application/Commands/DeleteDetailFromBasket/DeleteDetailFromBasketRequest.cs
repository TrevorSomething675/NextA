using MediatR;
using Nexta.Domain.Models.DataModels;

namespace Nexta.Application.Commands.DeleteDetailFromBasket
{
    public class DeleteDetailFromBasketRequest : IRequest<Result<DeleteDetailFromBasketResponse>>
	{
		public Guid UserId { get; set; }
		public Guid DetailId { get; set; }
	}
}
