using Nexta.Application.Commands.Orders.CreateNewOrderCommand;
using Nexta.Application.Queries.Orders.GetOrdersForUserQuery;
using Nexta.Web.Models.Orders;
using Nexta.Domain.Filters;
using AutoMapper;

namespace Nexta.Web.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<GetOrdersForUserRequest, GetOrdersFilter>();

            CreateMap<GetOrdersForUserRequest, GetOrdersForUserQuery>()
                .ForMember(src => src.Filter, opt => opt.MapFrom(x => x));

            CreateMap<CreateNewOrderRequest, CreateNewOrderCommand>();
        }
    }
}