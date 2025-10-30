using Nexta.Application.DTO.Baskets;
using Nexta.Domain.Models.Baskets;
using AutoMapper;

namespace Nexta.Application.Mappings
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}
