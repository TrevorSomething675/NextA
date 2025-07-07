namespace Nexta.Application.Commands.Admin.AddImageCommand
{
    public class AddAdminImageCommandResponse(Guid imageId)
    {
        public Guid ImageId { get; set; } = imageId;
    }
}
