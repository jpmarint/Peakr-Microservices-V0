using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.Models;

namespace SolicitudesAPI.Controllers
{
    [ApiController]
    [Route("api/company/requests")]
    public class RequestController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RequestController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //[HttpGet("Request")]
        //public async Task<ActionResult<RequestDetailDTO>> Get(int quoteId)
        //{

        //    var existe = await context.Quotes.AnyAsync(x => x.QuoteId == quoteId);

        //    if (!existe)
        //    {
        //        return NotFound("No existe esa quote");
        //    }

        //    var quote = await context.Quotes.Include(quotesDB => quotesDB.QuoteRequests)
        //        .ThenInclude(requestQuoteDB => requestQuoteDB.Request).ThenInclude(x => x.QuoteRequest)
        //        .FirstOrDefaultAsync(x => x.QuoteId == quoteId);

        //    return mapper.Map<RequestDetailDTO>(quote);
        //}

        [HttpPost]
        public async Task<IActionResult> Post(RequestCreationDTO requestCreationDTO)
        {          

            var request = mapper.Map<Request>(requestCreationDTO);
            context.Add(request);
            await context.SaveChangesAsync();
            var dto = mapper.Map<RequestDTO>(request);
            return Ok();
        }

        [HttpGet("RequestDetail")]
        public async Task<ActionResult<RequestDetailDTO>> GetDetail(int requestId)
        {

            var existe = await context.Requests.AnyAsync(x => x.RequestId == requestId);

            if (!existe)
            {
                return NotFound("No existe esa solicitud");
            }

            var request = await context.Requests.Include(requestDB => requestDB.QuoteRequest)
                .ThenInclude(requestQuoteDB => requestQuoteDB.Quote)
                .FirstOrDefaultAsync(x => x.RequestId == requestId);

            context.Entry(request).Reference(x => x.Address).Load();

            return mapper.Map<RequestDetailDTO>(request);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<List<RequestDTO>>> GetPendientes(int companyId)
        {
            var existe = await context.Companies.AnyAsync(x => x.CompanyId == companyId);

            if (!existe)
            {
                return NotFound("La compañia no existe en el sistema");
            }

            var request = await context.Requests.Include(x => x.Companies)
                .Where(requestDB => requestDB.CompanyId == companyId &&
            (requestDB.StatusRequest.Contains("por cotizar")) ||
            (requestDB.StatusRequest.Contains("por adjudicar")) ||
            (requestDB.StatusRequest.Contains("vencida")) ||
            (requestDB.StatusRequest.Contains("cerrada"))).ToListAsync();
            return mapper.Map<List<RequestDTO>>(request);
        }

        [HttpGet("sold")]
        public async Task<ActionResult<List<RequestDTO>>> GetVendidas(int companyId)
        {
            var existe = await context.Companies.AnyAsync(x => x.CompanyId == companyId);

            if (!existe)
            {
                return NotFound("La compañia no existe en el sistema");
            }

            var request = await context.Requests.Include(x => x.Companies)
                .Where(requestDB => requestDB.CompanyId == companyId &&
            (requestDB.StatusRequest.Contains("por enviar")) ||
            (requestDB.StatusRequest.Contains("en camino")) ||
            (requestDB.StatusRequest.Contains("entregado")) ||
            (requestDB.StatusRequest.Contains("Recibido"))).ToListAsync();
            return mapper.Map<List<RequestDTO>>(request);
        }



    }
}