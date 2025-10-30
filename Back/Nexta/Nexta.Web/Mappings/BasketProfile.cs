using Nexta.Application.Commands.Baskets.DeleteBasketProductCommand;
using Nexta.Application.Commands.Baskets.UpdateBasketProductCommand;
using Nexta.Application.Commands.Baskets.AddBasketProductCommand;
using Nexta.Web.Models.Basket;
using AutoMapper;

namespace Nexta.Web.Mappings
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
