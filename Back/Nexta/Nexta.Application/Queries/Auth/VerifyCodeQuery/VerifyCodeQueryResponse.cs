namespace Nexta.Application.Queries.Auth.VerifyCodeQuery
{
    public class VerifyCodeQueryResponse(string accessToken)
    {
        public string AccessToken { get; set; } = accessToken;
    }
}