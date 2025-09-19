using MediatR;

namespace Nexta.Application.Commands.Account.UpdateAccountCommand
{
    public class UpdateAccountCommand : IRequest<UpdateAccountCommandResponse>
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public string? Phone { get; set; }
    }
}