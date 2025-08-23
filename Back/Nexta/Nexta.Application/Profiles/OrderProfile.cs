using Nexta.Application.Commands.Admin.UpdateOrderCommand;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderResponse>();
            CreateMap<DateOnly, string>().ConvertUsing(d => d.ToString("dd.MM.yyyy"));

            CreateMap<PagedData<Order>, PagedData<OrderResponse>>();
            CreateMap<UpdateAdminOrderCommandRequest, Order>();
        }
    }
}