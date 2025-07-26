using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models.Images;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile() 
        {
            CreateMap<News, NewsEntity>().ReverseMap();

            CreateMap<NewsImage, NewsImageEntity>().ReverseMap();
        }
    }
}