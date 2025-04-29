using Nexta.Domain.Models;

namespace Nexta.Application.Commands.DeleteDetailFromBasket
{
    public class DeleteDetailFromBasketResponse(UserDetail userDetail)
    {
		public UserDetail UserDetail { get; set; } = userDetail;
	}
}
