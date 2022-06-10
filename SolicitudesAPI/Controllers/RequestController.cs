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

        /// <summary>
        /// Get New Request
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>

        [HttpGet("NewRequest", Name = "NewRequest")]
        public async Task<IActionResult> NewRequest(string search)
        {
            RequestSearchDTO requestSearchDTO = new RequestSearchDTO
            {
                QuerySearch = search,
                RequestDate = DateTime.Now
            };

            return Ok(requestSearchDTO);

        }

        /// <summary>
        /// Create a new request
        /// </summary>
        /// <param name="requestCreationDTO"></param>
        /// <returns></returns>

        [HttpPost("CreateRequest",Name = "CreateRequest")]
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
            return Ok(request);
        }

        /// <summary>
        /// Request Details as seller
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>

        [HttpGet("RequestDetailAsSeller", Name = "RequestDetailasSeller")]
        public async Task<ActionResult<RequestDetailSellerDTO>> GetDetailSeller(int requestId)
        {

            var noexiste = await context.Requests.AnyAsync(x => x.RequestId == requestId);

            if (!noexiste)
            {
                return NotFound("No existe esa solicitud");
            }

            var request = await context.Requests.Include(requestDB => requestDB.Quotes)
                .FirstOrDefaultAsync(x => x.RequestId == requestId);

            context.Entry(request).Reference(x => x.Address).Load();
            

            return mapper.Map<RequestDetailSellerDTO>(request);
        }

        /// <summary>
        /// Request Details as Buyer
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>

        [HttpGet("RequestDetailAsBuyer", Name = "RequestDetailAsBuyer")]
        public async Task<ActionResult<RequestDetailBuyerDTO>> GetDetailBuyer(int requestId)
        {

            var noexiste = await context.Requests.AnyAsync(x => x.RequestId == requestId);

            if (!noexiste)
            {
                return NotFound("No existe esa solicitud");
            }

            var request = await context.Requests.Include(requestDB => requestDB.Quotes)
                .FirstOrDefaultAsync(x => x.RequestId == requestId);

            context.Entry(request).Reference(x => x.Address).Load();


            return mapper.Map<RequestDetailBuyerDTO>(request);
        }

        /// <summary>
        /// Get All Requests As Seller
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>

        [HttpGet("AllRequestsAsSeller", Name = "AllRequestsAsSeller")]
        public async Task<ActionResult<List<RequestSellerDTO>>> GetAllRequestsAsSeller(int companyId)
        {
            var existe = await context.Companies.AnyAsync(x => x.CompanyId == companyId);

            if (!existe)
            {
                return NotFound("La compañia no existe en el sistema");
            }

            var request = await context.Requests.Include(x => x.Company)
                .Where(requestDB => requestDB.CompanyId == companyId).ToListAsync();

            return mapper.Map<List<RequestSellerDTO>>(request);
        }

        /// <summary>
        /// Get All Requests As Buyer
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>

        [HttpGet("AllRequestsAsBuyer", Name = "AllRequestsAsBuyer")]
        public async Task<ActionResult<List<RequestBuyerDTO>>> GetAllRequestsAsBuyer(int companyId)
        {
            var existe = await context.Companies.AnyAsync(x => x.CompanyId == companyId);

            if (!existe)
            {
                return NotFound("La compañia no existe en el sistema");
            }

            var requests = await context.Requests.Include(x => x.Company)
                .Where(requestDB => requestDB.CompanyId == companyId).ToListAsync();

            var result = mapper.Map<List<RequestBuyerDTO>>(requests);

            int qty = 0;

            foreach (var req in result)
            {
                qty = await context.QuoteRequest.Where(x => x.RequestId == req.RequestId).CountAsync();
                req.QuotesQty = qty;
                qty = 0;
            }

            return Ok(result);
        }



        }
}