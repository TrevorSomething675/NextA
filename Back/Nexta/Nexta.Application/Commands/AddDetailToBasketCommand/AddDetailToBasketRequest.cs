using Nexta.Domain.Models.DataModels;
using MediatR;

namespace Nexta.Application.Commands.AddDetailToBasketCommand
{
    public class AddDetailToBasketRequest : IRequest<Result<AddDetailToBasketResponse>>
    {
        public Guid UserId { get; set; }
        public Guid DetailId { get; set; }
    }
}