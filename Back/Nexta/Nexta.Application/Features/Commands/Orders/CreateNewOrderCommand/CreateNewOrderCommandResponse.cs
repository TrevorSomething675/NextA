namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
    public class CreateNewOrderCommandResponse(Guid id)
    {
        public Guid Id { get; set; } = id;
	}
}