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

        //[HttpGet("GetAddress")]
        //public async Task<ActionResult<AddressDTO>> GetAddress(int addressId)
        //{

        //    var address = await context.Address
        //        .Where(addressDB => addressDB.AddressId == addressId).FirstOrDefaultAsync();

        //    return mapper.Map<AddressDTO>(address);

        //}

        [HttpGet("NewRequest", Name = "NewRequest")]
        public async Task<IActionResult> NewRequest(string search, int companyId)
        {

            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }

            var companyAddressId = await context.Companies
                .Where(x => x.CompanyId == companyId)
                .Select(u => u.AddressId)
                .FirstOrDefaultAsync();

            var address = await context.Address
                .Where(addressDB => addressDB.AddressId == companyAddressId).FirstOrDefaultAsync();

            RequestSearchDTO requestSearchDTO = new RequestSearchDTO
            {
                QuerySearch = search,
                RequestDate = DateTime.Now,
                Department = address.Department,
                City =  address.City,
                Line1 = address.Line1,
                Line2 = address.Line2,
                PostalCode = address.PostalCode,               
                Notes = address.Notes
            };

            return Ok(requestSearchDTO);

        }

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

            var request = await context.Requests.Include(requestDB => requestDB.Quotes)
                .FirstOrDefaultAsync(x => x.RequestId == requestId);

            context.Entry(request).Reference(x => x.Address).Load();
            

            return mapper.Map<RequestDetailDTO>(request);
        }


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

        ///// <summary>
        ///// requests approved and purchase order placed
        ///// </summary>
        //// <param name="companyId"></param>
        ///// <returns></returns>

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