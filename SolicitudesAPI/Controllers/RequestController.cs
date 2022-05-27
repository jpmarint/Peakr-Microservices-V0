using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.Models;

namespace SolicitudesAPI.Controllers
{
    [ApiController]
    [Route("api/company/{companyId:int}/requests")]
    public class RequestController : ControllerBase
    {    
        private readonly SolicitudesAPIContext context;
        private readonly IMapper mapper;

        public RequestController(SolicitudesAPIContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("pending")]
        public async Task<ActionResult<List<RequestDTO>>> GetPendientes(int companyId)
        {
            var existe = await context.Companies.AnyAsync(x => x.CompanyId == companyId);

            if (!existe)
            {
                return NotFound("La compañia no existe en el sistema");
            }

            var request = await context.Requests.Include(x => x.Company)
                .Where(requestDB => requestDB.CompanyId == companyId &&
            (requestDB.RequestStatus.Contains("por cotizar")) ||
            (requestDB.RequestStatus.Contains("por adjudicar")) ||
            (requestDB.RequestStatus.Contains("vencida")) ||
            (requestDB.RequestStatus.Contains("cerrada"))).ToListAsync();
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

            var request = await context.Requests.Include(x => x.Company).Where(requestDB => requestDB.CompanyId == companyId &&
            (requestDB.RequestStatus.Contains("por enviar")) ||
            (requestDB.RequestStatus.Contains("en camino")) ||
            (requestDB.RequestStatus.Contains("entregado")) ||
            (requestDB.RequestStatus.Contains("recibido"))).ToListAsync();
            return mapper.Map<List<RequestDTO>>(request);
        }

       

    }
}
