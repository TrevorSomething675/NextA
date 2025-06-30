using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailResponse>();
        }
    }
}