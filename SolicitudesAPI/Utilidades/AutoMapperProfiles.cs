using AutoMapper;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.Models;

namespace SolicitudesAPI.Utilidades
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {

            CreateMap<Address, AddressDTO>().ReverseMap();

            CreateMap<Quote, QuoteDTO2>().ReverseMap();
            CreateMap<Request, RequestModalDTO>()
                .ForMember(x => x.City, x => x.MapFrom(y => y.Address.City))
                .ForMember(x => x.Department, x => x.MapFrom(y => y.Address.Department))
                .ForMember(x => x.Line2, x => x.MapFrom(y => y.Address.Line2))
                .ForMember(x => x.PostalCode, x => x.MapFrom(y => y.Address.PostalCode))
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Companies.Name))
                .ForMember(x => x.LogoPath, x => x.MapFrom(y => y.Companies.LogoPath))
                .ForMember(x => x.WebSiteUrl, x => x.MapFrom(y => y.Companies.WebSiteUrl));

            CreateMap<RequestCreationDTO, Request>()
                .ForMember(request => request.requestCategories, opciones => opciones.MapFrom(MapRequestCategory));

            CreateMap<Quote, QuoteDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<CompanyCreationDTO, Company>()
                .ForMember(x => x.LegalExistenceDocPath, options => options.Ignore())
                .ForMember(x => x.BankAccountDocPath, options => options.Ignore())
                .ForMember(x => x.RutDocPath, options => options.Ignore());

            CreateMap<QuoteRequest, QuoteRequestDTO>().ReverseMap();

            CreateMap<Request, RequestDTO>()
                .ForMember(x => x.CompanyName, x => x.MapFrom(y => y.Companies.Name))
                .ForMember(x => x.LogoPath, x => x.MapFrom(y => y.Companies.LogoPath))
                .ForMember(x => x.WebSiteUrl, x => x.MapFrom(y => y.Companies.WebSiteUrl));

            CreateMap<QuoteCreationDTO, Quote>()
                .ForMember(quote => quote.QuoteRequests, opciones => opciones.MapFrom(MapQuoteRequest));

            CreateMap<Request, RequestDetailDTO>()
                .ForMember(x => x.City, x => x.MapFrom(y => y.Address.City))
                .ForMember(requestDetailDTO => requestDetailDTO.Quotes, opciones => opciones.MapFrom(MapRequestQuotesList));
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
            if (request.QuoteRequest == null) { return res; }
            foreach (var requestQuote in request.QuoteRequest)
            {
                res.Add(new QuoteDTO()
                {
                    QuoteId = requestQuote.QuoteId,
                    QuoteProductName = requestQuote.Quote.QuoteProductName,
                    DeliveryDeadLineInDays = requestQuote.Quote.DeliveryDeadLineInDays,
                    QuoteExpirationDate = requestQuote.Quote.QuoteExpirationDate,
                    NetCost = requestQuote.Quote.NetCost

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
                result.Add(new QuoteRequest() { RequestId = quoteCreationDTO.RequestId });
            
            return result;
        }
    }

    }


