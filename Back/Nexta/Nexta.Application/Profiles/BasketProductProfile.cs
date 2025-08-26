using Nexta.Application.Commands.Basket.UpdateBasketProductCommand;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class BasketProductProfile : Profile
    {
        public BasketProductProfile() 
        {
            CreateMap<BasketProduct, BasketProductResponse>();
            CreateMap<BasketProduct, UpdateBasketProductCommandResponse>();
        }
    }
}