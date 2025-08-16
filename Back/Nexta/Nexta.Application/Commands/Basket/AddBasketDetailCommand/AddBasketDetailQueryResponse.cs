using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Basket.AddBasketDetailCommand
{
    public class AddBasketDetailQueryResponse(UserDetailResponse userDetail)
    {
        public UserDetailResponse UserDetail { get; set; } = userDetail;
	}
}