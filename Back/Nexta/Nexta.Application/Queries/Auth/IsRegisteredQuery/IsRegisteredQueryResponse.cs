namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
    public class IsRegisteredQueryResponse(bool exist)
    {
        public bool Exist { get; set; } = exist;
    }
}