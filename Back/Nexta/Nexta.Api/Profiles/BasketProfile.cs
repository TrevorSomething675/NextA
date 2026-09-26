using Nexta.Application.Commands.Basket.DeleteBasketProductCommand;
using Nexta.Application.Commands.Basket.UpdateBasketProductCommand;
using Nexta.Application.Commands.Basket.AddBasketProductCommand;
using Nexta.Web.Models.Basket;
using AutoMapper;

namespace Nexta.Web.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<AddBasketProductRequest, AddBasketProductCommand>();
            CreateMap<DeleteBasketProductRequest, DeleteBasketProductCommand>();
            CreateMap<UpdateBasketProductRequest, UpdateBasketProductCommand>();
        }
    }
}
