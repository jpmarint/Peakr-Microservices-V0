using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs;
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

        

            [HttpGet]
            public async Task<ActionResult<QuoteRequestDTO>> Get(int quoteId, int requestId)
        {
            var quoteRequest = await context.QuoteRequest.Include(request => request.Quote)
                .Include(quote => quote.Request)
                .Where(q => q.QuoteId == quoteId && q.RequestId == requestId).FirstOrDefaultAsync();

            var city = quoteRequest.Request;
            var company = quoteRequest.Request;

            context.Entry(city).Reference(x => x.Address).Load();
            context.Entry(city).Reference(x => x.Companies).Load();

            //return Ok(quoteRequest);
            return mapper.Map<QuoteRequestDTO>(quoteRequest);
        }

        //[HttpGet]
        //public async Task<ActionResult<RequestDetailDTO>> Get(int id)
        //{

        //    var request = await context.Requests.Include(requestBD => requestBD.Quotes)
        //        .FirstOrDefaultAsync(x => x.RequestId == id);

        //    context.Entry(request).Reference(x => x.Adress).Load();

        //    var company = new Company();

        //    context.Entry(company).Collection(x => x.Requests).Load();

        //    return mapper.Map<RequestDetailDTO>(request);
        //}

        [HttpPost]
        public async Task<IActionResult> Post(QuoteCreationDTO quoteCreationDTO)
        {
            if (quoteCreationDTO.RequestId == null)
            {
                return BadRequest("No se puede crear una cotizacion sin solicitud");
            }

            var requestId = await context.Requests
                .Where(requestDB => quoteCreationDTO.RequestId
                .Contains(requestDB.RequestId)).Select(x => x.RequestId).ToListAsync();

            if (quoteCreationDTO.RequestId.Count != requestId.Count)
            {
                return BadRequest("No existe la solicitud enviada");
            }

            var quote = mapper.Map<Quote>(quoteCreationDTO);
            context.Add(quote);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetRequestCreationQuote")]
        public async Task<ActionResult<RequestModalDTO>> Get(int requestId)
        {
            var request = await context.Requests               
                .Where(x => x.RequestId == requestId).FirstOrDefaultAsync();

           
            context.Entry(request).Reference(x => x.Address).Load();
            context.Entry(request).Reference(x => x.Companies).Load();

            //return Ok(quoteRequest);
            return mapper.Map<RequestModalDTO>(request);
        }

    }
}
