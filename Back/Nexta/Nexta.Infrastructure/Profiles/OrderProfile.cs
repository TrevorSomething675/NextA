using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderEntity, Order>().ReverseMap();
            CreateMap<PagedData<OrderEntity>, PagedData<Order>>();
        }
    }
}