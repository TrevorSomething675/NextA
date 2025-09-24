using Nexta.Application.Queries.Admin.GetUsersQuery;
using Nexta.Domain.Filters.Users;
using Nexta.Web.Models.Users;
using AutoMapper;

namespace Nexta.Web.Profiles.Admin
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GetUsersRequest, GetAdminUsersFilter>()
                .ForMember(src => src.SearchTerm, opt => opt.MapFrom(x => x.SearchTerm ?? ""));
            CreateMap<GetUsersRequest, GetUsersQuery>()
                .ForMember(src => src.Filter, opt => opt.MapFrom(x => x));
        }
    }
}