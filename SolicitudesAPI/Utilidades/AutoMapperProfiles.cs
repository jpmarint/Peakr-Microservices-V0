using AutoMapper;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.DTOs.CompanyDTOs;
using SolicitudesAPI.DTOs.QuoteDTOs;
using SolicitudesAPI.DTOs.RequestDTOs;
using SolicitudesAPI.Models;

namespace SolicitudesAPI.Utilidades
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {

            //Address

            CreateMap<Address, AddressDTO>().ReverseMap();

            //Categories

            CreateMap<Category, CategoryDTO>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.CategoryId))
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Name));

            //Requests
            
            CreateMap<Request, RequestModalDTO>()
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Company.Name))
                .ForMember(x => x.LogoPath, x => x.MapFrom(y => y.Company.LogoKey));

            CreateMap<RequestCreationDTO, Request>()
                .ForMember(request => request.Categories, opciones => opciones.MapFrom(MapRequestCategory));
            CreateMap<Request, RequestDTO>()
               .ForMember(x => x.CompanyName, x => x.MapFrom(y => y.Company.Name))
               .ForMember(x => x.LogoPath, x => x.MapFrom(y => y.Company.LogoKey))
               .ForMember(x => x.WebSiteUrl, x => x.MapFrom(y => y.Company.WebSiteUrl));

            CreateMap<Request, RequestDetailDTO>()
            .ForMember(x => x.City, x => x.MapFrom(y => y.Address.City))
            .ForMember(x => x.Department, x => x.MapFrom(y => y.Address.Department));

            //company

            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Company, CompanyDetailsDTO>().ReverseMap();
            CreateMap<Company, CompanyDocsDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<CompanyCreationDTO, Company>()
                .ForMember(x => x.LegalExistenceDocGuid, options => options.Ignore())
                .ForMember(x => x.BankAccountDocGuid, options => options.Ignore())
                .ForMember(x => x.RutDocGuid, options => options.Ignore());

            //Quotes

            CreateMap<QuoteDTO, Quote>();

            CreateMap<Quote, QuoteDTO>()
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Company.Name))
                .ForMember(x => x.WebSiteUrl, x => x.MapFrom(y => y.Company.WebSiteUrl));

            CreateMap<Quote, QuoteReadOnlyDTO>().ReverseMap();

            CreateMap<QuoteRequest, QuoteRequestDTO>().ReverseMap();

            CreateMap<QuoteCreationDTO, Quote>()
                .ForMember(quote => quote.QuoteRequest, opciones => opciones.MapFrom(MapQuoteRequest));

        }

        //metodos


        //metodo para mapear requestcategory

        private List<RequestCategory> MapRequestCategory(RequestCreationDTO requestCreationDTO, Request request)
        {
            var resultado = new List<RequestCategory>();

            if (requestCreationDTO.CategoriesIds == null)
            {
                return resultado;
            }

            foreach (var categoryId in requestCreationDTO.CategoriesIds)
            {
                resultado.Add(new RequestCategory() { CategoryId = categoryId });
            }

            return resultado;
        }



        private List<QuoteDTO> MapRequestQuotesList(Request request, RequestDetailDTO requestDetailDTO)
        {
            var res = new List<QuoteDTO>();
            if (request.Quotes == null) { return res; }
            foreach (var requestQuote in request.Quotes)
            {
                res.Add(new QuoteDTO()
                {
                    QuoteId = requestQuote.QuoteId,
                    QuoteProductName = requestQuote.QuoteProductName,
                    DeliveryDeadLineInDays = requestQuote.DeliveryDeadLineInDays,
                    QuoteExpirationDate = requestQuote.QuoteExpirationDate,
                    NetCost = requestQuote.NetCost

                });
            }

            return res;
        }

        private List<QuoteRequest> MapQuoteRequest(QuoteCreationDTO quoteCreationDTO, Quote quote)
        {

            var result = new List<QuoteRequest>();

            if (quoteCreationDTO.RequestId == null)
            {
                return result;
            }

                result.Add(new QuoteRequest() {

                    RequestId = quoteCreationDTO.RequestId
                });

            return result;
        }
    }

    }


