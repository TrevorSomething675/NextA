using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderEntity>().ReverseMap();

            CreateMap<PagedData<Order>, PagedData<OrderEntity>>().ReverseMap();
        }
    }
}