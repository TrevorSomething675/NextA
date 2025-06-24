namespace Nexta.Application.Queries.Auth.SendVerificationCodeQuery
{
    public class SendVerificationCodeQueryResponse(bool isSuccess)
    {
        public bool IsSuccess { get; set; } = isSuccess;
	}
}