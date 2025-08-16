using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Application.Commands.Admin.UpdateOrderCommand;
using Nexta.Application.DTO.Response;

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