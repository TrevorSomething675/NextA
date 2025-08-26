using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Basket.AddBasketProductCommand
{
    public class AddBasketProductCommandResponse(BasketProduct basketProduct)
    {
        public BasketProduct BasketProduct { get; set; } = basketProduct;
	}
}