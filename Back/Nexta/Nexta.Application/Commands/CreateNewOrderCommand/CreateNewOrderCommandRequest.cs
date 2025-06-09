using Nexta.Domain.Models.DataModels;
using MediatR;

namespace Nexta.Application.Commands.CreateNewOrderCommand
{
    public class CreateNewOrderCommandRequest : IRequest<Result<CreateNewOrderCommandResponse>>
    {
        public Guid UserId { get; set; }
        public List<Guid> DetailIds { get; set; }
    }
}