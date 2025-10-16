using Nexta.Application.Commands.Admin.UpdateOrderCommand;
using Nexta.Application.DTO.Response;
using AutoMapper;
using Nexta.Domain.Base;
using Nexta.Domain.Models.Order;

namespace Nexta.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderResponse>();
            CreateMap<DateOnly, string>().ConvertUsing(d => d.ToString("dd.MM.yyyy"));

            CreateMap<PagedData<Order>, PagedData<OrderResponse>>();
            CreateMap<UpdateAdminOrderCommand, Order>();
        }
    }
}