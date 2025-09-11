namespace Nexta.Application.Commands.Admin.CreateAdminProductCommand
{
    public class CreateAdminProductCommandResponse(Guid id)
    {
        public Guid Id { get; init; } = id;
    }
}