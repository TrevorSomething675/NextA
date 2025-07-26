namespace Nexta.Application.Commands.Admin.DeleteNewsCommand
{
    public class DeleteNewsCommandResponse(Guid id)
    {
        public Guid Id { get; init; } = id;
    }
}
