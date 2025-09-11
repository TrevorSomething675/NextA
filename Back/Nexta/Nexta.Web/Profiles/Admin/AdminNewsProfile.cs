using Nexta.Application.Commands.Admin.AddNewsCommand;
using Nexta.Application.DTO.Request;
using Nexta.Web.Areas.Models;
using AutoMapper;

namespace Nexta.Web.Profiles.Admin
{
    public class AdminNewsProfile : Profile
    {
        public AdminNewsProfile()
        {
            CreateMap<AddNewsRequest, AddNewsCommand>()
                .ForMember(src => src.Image, opt => opt.MapFrom(x =>
                    new NewsImageRequest
                    {
                        Name = x.ImageName,
                        Base64String = x.ImageBase64String
                    }));
        }
    }
}
