using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.DTOs.RequestDTOs;
using SolicitudesAPI.Models;
using SolicitudesAPI.Servicios;

namespace SolicitudesAPI.Controllers
{

    [ApiController]
    [Route("api/company/request")]
    public class RequestController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "request";

        public RequestController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDTO>> GetAddress(int addressId)
        {

            var address = await context.Address
                .Where(addressDB => addressDB.AddressId == addressId).FirstOrDefaultAsync();

            return mapper.Map<AddressDTO>(address);

        }

        //[HttpPost("NewRequest", Name = "NewRequest")]
        //public async Task<IActionResult> NewRequest(string search)
        //{
        //    var resultado1 = search;    

        //    return Ok(resultado1);

        //}

        /// <summary>
        /// Register a new request
        /// </summary>
        // <param name="requestCreationDTO"></param>
        /// <returns></returns>
        /// 

        [HttpPost("PostRequest",Name = "PostRequest")]
        public async Task<IActionResult> Post(RequestCreationDTO requestCreationDTO)
        {

            if (requestCreationDTO.CategoriesIds == null)
            {
                return BadRequest("No se puede crear una request sin categoría");
            }

            var categoryId = await context.Categories
                .Where(categoryDB => requestCreationDTO.CategoriesIds.Contains(categoryDB.CategoryId))
                .Select(x => x.CategoryId).ToListAsync();

            if (requestCreationDTO.CategoriesIds.Count != categoryId.Count)
            {
                return BadRequest("No existe una de las categorias enviadas");
            }



            var request = mapper.Map<Request>(requestCreationDTO);
            context.Add(request);
            await context.SaveChangesAsync();
            var dto = mapper.Map<RequestDTO>(request);
            return Ok(request);
        }

        /// <summary>
        /// Request Details as seller
        /// </summary>
        // <param name="requestId"></param>
        /// <returns></returns>
     
        [HttpGet("RequestDetail", Name = "RequestDetailasSeller")]
        public async Task<ActionResult<RequestDetailDTO>> GetDetail(int requestId)
        {

            var noexiste = await context.Requests.AnyAsync(x => x.RequestId == requestId);

            if (!noexiste)
            {
                return NotFound("No existe esa solicitud");
            }

            var request = await context.Requests.Include(requestDB => requestDB.QuoteRequest)
                .ThenInclude(requestQuoteDB => requestQuoteDB.Quote)
                .FirstOrDefaultAsync(x => x.RequestId == requestId);

            context.Entry(request).Reference(x => x.Address).Load();

            return mapper.Map<RequestDetailDTO>(request);
        }

        /// <summary>
        /// pending requests without quote
        /// </summary>
        // <param name="companyId"></param>
        /// <returns></returns>

        [HttpGet("Pending", Name = "RequestAsSeller/Pending")]
        public async Task<ActionResult<List<RequestDTO>>> GetPending(int companyId)
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

        /// <summary>
        /// requests approved and purchase order placed
        /// </summary>
        // <param name="companyId"></param>
        /// <returns></returns>
 
        [HttpGet("Sold", Name = "RequestAsSeller/Sold")]
        public async Task<ActionResult<List<RequestDTO>>> GetSold(int companyId)
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