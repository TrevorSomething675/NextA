using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class OrderProductProfile : Profile
    {
        public OrderProductProfile()
        {
            CreateMap<OrderProductEntity, OrderProduct>().ReverseMap();
        }
    }
}
