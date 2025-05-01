using Nexta.Domain.Models;

namespace Nexta.Application.Commands.DeleteDetailFromBasket
{
    public class DeleteBasketDetailCommandResponse(UserDetail userDetail)
    {
		public UserDetail UserDetail { get; set; } = userDetail;
	}
}