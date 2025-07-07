using Nexta.Application.Commands.Admin.AddImageCommand;
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
            CreateMap<AddAdminImageCommandRequest, Image>();
            CreateMap<ImageResponse, Image>();
        }
    }
}