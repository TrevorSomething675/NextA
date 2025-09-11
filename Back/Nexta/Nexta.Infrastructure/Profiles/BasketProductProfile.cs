using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class BasketProductProfile : Profile
    {
        public BasketProductProfile()
        {
            CreateMap<BasketProductEntity, BasketProduct>().ReverseMap();
        }
    }
}