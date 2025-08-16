using Nexta.Application.Commands.Admin.AddNewsCommand;
using Nexta.Domain.Models.Images;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Application.DTO.Request;
using Nexta.Application.DTO.Response;

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