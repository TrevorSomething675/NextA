using Nexta.Application.Queries.Admin.GetUsersQuery;
using Nexta.Web.Models.Users;
using AutoMapper;

namespace Nexta.Web.Mappings.Admin
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GetUsersRequest, GetUsersQuery>();
        }
    }
}