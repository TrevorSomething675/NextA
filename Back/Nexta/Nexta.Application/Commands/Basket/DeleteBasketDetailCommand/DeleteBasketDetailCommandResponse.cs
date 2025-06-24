using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Basket.DeleteBasketDetailCommand
{
    public class DeleteBasketDetailCommandResponse(UserDetail userDetail)
    {
		public UserDetail UserDetail { get; set; } = userDetail;
	}
}