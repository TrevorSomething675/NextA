namespace Nexta.Application.Commands.Admin.UpdateOrderCommand
{
    public class UpdateAdminOrderCommandResponse(Guid id)
    {
        public Guid Id { get; init; } = id;
    }
}