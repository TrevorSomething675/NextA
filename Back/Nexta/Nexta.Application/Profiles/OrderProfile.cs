using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Application.Commands.Admin.UpdateOrderCommand;

namespace Nexta.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderResponse>();
            CreateMap<PagedData<Order>, PagedData<OrderResponse>>();
            CreateMap<UpdateAdminOrderCommandRequest, Order>();
        }
    }
}