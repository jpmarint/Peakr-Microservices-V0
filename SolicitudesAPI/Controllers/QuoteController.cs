using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs.QuoteDTOs;
using SolicitudesAPI.DTOs.RequestDTOs;
using SolicitudesAPI.Models;
using System.Linq;

namespace SolicitudesAPI.Controllers
{

    [ApiController]
    [Route("api/quotes")]
    public class QuoteController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public QuoteController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        /// <summary>
        /// Quote details
        /// </summary>
        /// <param name="quoteId"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        /// 
       
        [HttpGet("QuoteDetails", Name = "Request&QuoteReadSellerModal")]
            public async Task<ActionResult<QuoteRequestDTO>> Get(int quoteId, int requestId)
        {


            var requestnoexiste = await context.Requests.AnyAsync(x => x.RequestId == requestId);

            if (!requestnoexiste)
            {
                return NotFound("No existe esa solicitud");
            }


            var quotenoexiste = await context.Quotes.AnyAsync(x => x.QuoteId == quoteId);

            if (!quotenoexiste)
            {
                return NotFound("No existe esa cotización");
            }


            var quoteRequest = await context.QuoteRequest.Include(request => request.Quote)
                .Include(quote => quote.Request)
                .Where(q => q.QuoteId == quoteId && q.RequestId == requestId).FirstOrDefaultAsync();

            var city = quoteRequest.Request;
            var company = quoteRequest.Request;

            context.Entry(city).Reference(x => x.Address).Load();
            context.Entry(company).Reference(x => x.Company).Load();

            
            return mapper.Map<QuoteRequestDTO>(quoteRequest);
        }


        /// <summary>
        /// Register a new Quote
        /// </summary>
        /// <param name="quoteCreationDTO"></param>
        /// <returns></returns>
       
        [HttpPost("NewQuote", Name = "QuoteCreationModal")]
        public async Task<IActionResult> Post(QuoteCreationDTO quoteCreationDTO)
        {
            if (quoteCreationDTO.RequestId == null)
            {
                return BadRequest("No se puede crear una cotizacion sin solicitud");
            }
            var requestId = await context.Requests
                .Where(requestDB => requestDB.RequestId == quoteCreationDTO.RequestId)
                .Select(x => x.RequestId).ToListAsync();

            var quote = mapper.Map<Quote>(quoteCreationDTO);
            context.Add(quote);
            await context.SaveChangesAsync();
            return Ok(quote);
        }

        /// <summary>
        /// Request information to create a new quote
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
     
        [HttpGet("requestNewQuote", Name = "GetRequestCreationQuote")]
        public async Task<ActionResult<RequestModalDTO>> Get(int requestId)
        {

            var requestnoexiste = await context.Requests.AnyAsync(x => x.RequestId == requestId);

            if (!requestnoexiste)
            {
                return NotFound("No existe esa solicitud");
            }

            var request = await context.Requests               
                .Where(x => x.RequestId == requestId).FirstOrDefaultAsync();

           
            context.Entry(request).Reference(x => x.Address).Load();
            context.Entry(request).Reference(x => x.Company).Load();

           
            return mapper.Map<RequestModalDTO>(request);
        }

    }
}
