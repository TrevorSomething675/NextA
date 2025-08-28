using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Basket.AddBasketProductCommand
{
    public class AddBasketProductCommandResponse(BasketProductResponse basketProduct)
    {
        public BasketProductResponse BasketProduct { get; set; } = basketProduct;
	}
}