using Nexta.Application.Commands.Admin.AddNewsCommand;
using Nexta.Application.DTO.RequestModels;
using Nexta.Domain.Models.Images;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile() 
        {
            CreateMap<AddNewsCommandRequest, News>();
            CreateMap<NewsImageRequest, NewsImage>();
            CreateMap<News, NewsResponse>();
            CreateMap<NewsImage, NewsImageResponse>();
        }
    }
}