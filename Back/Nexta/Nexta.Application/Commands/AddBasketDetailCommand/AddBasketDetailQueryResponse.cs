using Nexta.Domain.Models;

namespace Nexta.Application.Commands.AddDetailToBasketCommand
{
    public class AddBasketDetailQueryResponse(Detail detail)
    {
        public Detail Detail { get; set; } = detail;
    }
}