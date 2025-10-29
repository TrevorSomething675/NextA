using Nexta.Application.Commands.Orders.CreateNewOrderCommand;
using Nexta.Application.Queries.Orders.GetOrdersForUserQuery;
using Nexta.Web.Models.Orders;
using AutoMapper;

namespace Nexta.Web.Mappings
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {

            CreateMap<GetOrdersForUserRequest, GetOrdersForUserQuery>();

            CreateMap<CreateNewOrderRequest, CreateNewOrderCommand>();
        }
    }
}