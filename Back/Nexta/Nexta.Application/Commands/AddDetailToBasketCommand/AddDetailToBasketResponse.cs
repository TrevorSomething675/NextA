using Nexta.Domain.Models;

namespace Nexta.Application.Commands.AddDetailToBasketCommand
{
    public class AddDetailToBasketResponse(UserDetail userDetail)
    {
        public UserDetail UserDetail { get; set; } = userDetail;
    }
}