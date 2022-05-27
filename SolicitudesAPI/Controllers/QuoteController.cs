using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.Models;

namespace SolicitudesAPI.Controllers
{
    [ApiController]
    [Route("api/request/DetailRequest")]
    public class QuoteController : ControllerBase
    {
        private readonly SolicitudesAPIContext context;
        private readonly IMapper mapper;

        public QuoteController(SolicitudesAPIContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RequestDetailDTO>> Get(int id)
        {

            var request = await context.Requests.Include(requestBD => requestBD.Quotes)
                .FirstOrDefaultAsync(x => x.RequestId == id);

            context.Entry(request).Reference(x => x.Adress).Load();

            //var company = new Company();

            //context.Entry(company).Collection(x => x.Requests).Load();

            return mapper.Map<RequestDetailDTO>(request);                                    
        }

    }
}
