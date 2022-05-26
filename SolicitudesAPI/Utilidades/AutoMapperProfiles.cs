using AutoMapper;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.Models;

namespace SolicitudesAPI.Utilidades
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<CompanyCreationDTO, Company>()
                .ForMember(x => x.LogoPath, options => options.Ignore());


            CreateMap<Request, RequestDTO>()
                .ForMember(x => x.CompanyName, x => x.MapFrom(y => y.Company.CompanyName))
                .ForMember(x => x.LogoPath, x => x.MapFrom(y => y.Company.LogoPath))
                .ForMember(x => x.WebSiteUrl, x => x.MapFrom(y => y.Company.WebSiteUrl));

            

            CreateMap<Quote, QuoteDTO>().ReverseMap();
            CreateMap<Request, RequestDetailDTO>()
                .ForMember(x => x.City, x => x.MapFrom(y => y.Adress.City));
        }

    }
}
