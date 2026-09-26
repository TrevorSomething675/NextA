using Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand;
using Nexta.Application.Commands.Admin.UpdateOrderCommand;
using Nexta.Application.Queries.Admin.GetAllOrdersQuery;
using Nexta.Web.Areas.Models;
using Nexta.Domain.Filters;
using AutoMapper;

namespace Nexta.Web.Profiles.Admin
{
    public class AdminOrdersProfile : Profile
    {
        public AdminOrdersProfile()
        {
            CreateMap<GetAdminOrdersRequest, GetAdminOrdersQuery>()
                .ForMember(src => src.Filter, opt => opt.MapFrom(x => x));

            CreateMap<GetAdminOrdersRequest, GetOrdersFilter>()
                .ForMember(src => src.SearchTerm, opt => opt.MapFrom(x => x.SearchTerm ?? ""));

            CreateMap<UpdateAdminOrderRequest, UpdateAdminOrderCommand>();

            CreateMap<DeleteProductFromOrderRequest, DeleteProductFromOrderCommand>();
        }
    }
}
