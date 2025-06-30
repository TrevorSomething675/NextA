using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetailEntity, OrderDetail>().ReverseMap();
        }
    }
}