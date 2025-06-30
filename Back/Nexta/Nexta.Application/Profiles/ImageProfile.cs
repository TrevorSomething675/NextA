using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile() 
        {
            CreateMap<Image, ImageResponse>();
        }
    }
}