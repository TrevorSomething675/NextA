using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Basket.AddBasketDetailCommand
{
    public class AddBasketDetailQueryResponse(Detail detail)
    {
        public Detail Detail { get; set; } = detail;
    }
}