namespace Nexta.Application.Commands.Account.ConfirmPhoneCommand
{
    public class ConfirmPhoneCommandResponse(string phone)
    {
        public string Phone { get; init; } = phone;
    }
}
