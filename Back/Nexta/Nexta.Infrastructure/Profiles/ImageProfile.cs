using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models.Images;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ProductImage, ProductImageEntity>().ReverseMap();
        }
    }
}