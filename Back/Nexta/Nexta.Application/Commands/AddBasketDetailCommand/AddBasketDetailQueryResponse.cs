using Nexta.Domain.Models;

namespace Nexta.Application.Commands.AddDetailToBasketCommand
{
    public class AddBasketDetailQueryResponse(UserDetail userDetail)
    {
        public UserDetail UserDetail { get; set; } = userDetail;
    }
}