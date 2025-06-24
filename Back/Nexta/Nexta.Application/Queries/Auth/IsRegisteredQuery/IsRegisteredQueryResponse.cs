namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
    public class IsRegisteredQueryResponse(bool isRegistered)
    {
        public bool IsRegistered { get; set; } = isRegistered;
    }
}