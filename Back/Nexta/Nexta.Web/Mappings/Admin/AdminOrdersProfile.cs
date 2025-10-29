using Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand;
using Nexta.Application.Commands.Admin.UpdateOrderCommand;
using Nexta.Application.Queries.Admin.GetAllOrdersQuery;
using Nexta.Web.Areas.Models;
using AutoMapper;

namespace Nexta.Web.Mappings.Admin
{
    public class AdminOrdersProfile : Profile
    {
        public AdminOrdersProfile()
        {
            CreateMap<GetAdminOrdersRequest, GetAdminOrdersQuery>();

            CreateMap<UpdateAdminOrderRequest, UpdateAdminOrderCommand>();

            CreateMap<DeleteProductFromOrderRequest, DeleteProductFromOrderCommand>();
        }
    }
}
