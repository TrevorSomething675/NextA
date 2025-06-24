namespace Nexta.Application.Queries.AuthQueries.CheckRegisterUserQuery
{
    public class CheckRegisterUserQueryResponse(bool isRegistered)
    {
        public bool IsRegistered { get; set; } = isRegistered;
    }
}