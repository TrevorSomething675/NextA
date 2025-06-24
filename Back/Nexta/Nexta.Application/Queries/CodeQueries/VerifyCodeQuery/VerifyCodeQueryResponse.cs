namespace Nexta.Application.Queries.CodeQueries.VerifyCodeQuery
{
    public class VerifyCodeQueryResponse(string accessToken)
    {
        public string AccessToken { get; set; } = accessToken;
    }
}