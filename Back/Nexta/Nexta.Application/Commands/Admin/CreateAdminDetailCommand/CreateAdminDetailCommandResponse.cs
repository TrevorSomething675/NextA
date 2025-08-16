namespace Nexta.Application.Commands.Admin.CreateAdminDetailCommand
{
    public class CreateAdminDetailCommandResponse(Guid id)
    {
        public Guid Id { get; init; } = id;
    }
}