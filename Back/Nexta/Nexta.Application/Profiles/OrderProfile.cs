using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderResponse>();
            CreateMap<PagedData<Order>, PagedData<OrderResponse>>();
        }
    }
}