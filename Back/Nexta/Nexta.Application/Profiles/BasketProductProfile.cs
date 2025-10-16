using Nexta.Application.Commands.Basket.UpdateBasketProductCommand;
using Nexta.Application.DTO.Response;
using AutoMapper;
using Nexta.Domain.Models.Basket;

namespace Nexta.Application.Profiles
{
    public class BasketProductProfile : Profile
    {
        public BasketProductProfile() 
        {
            CreateMap<BasketProduct, BasketProductResponse>();
            CreateMap<BasketProduct, UpdateBasketProductCommandResponse>();
            CreateMap<BasketProduct, BasketProductResponse>()
                .ForMember(src => src.ProductId, opt => opt.MapFrom(x => x.ProductId))
                .ForMember(src => src.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(src => src.Count, opt => opt.MapFrom(x => x.Count))
                .ForMember(src => src.DeliveryDate, opt => opt.MapFrom(x => x.DeliveryDate))
                .ForMember(src => src.Article, opt => opt.MapFrom(x => x.Product!.Article))
                .ForMember(src => src.Name, opt => opt.MapFrom(x => x.Product!.Name))
                .ForMember(src => src.NewPrice, opt => opt.MapFrom(x => x.Product!.NewPrice))
                .ForMember(src => src.OldPrice, opt => opt.MapFrom(x => x.Product!.OldPrice ?? null))
                .ForMember(src => src.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(src => src.Description, opt => opt.MapFrom(x => x.Product!.Description));
            CreateMap<UpdateBasketProductCommand, BasketProduct>();
        }
    }
}