﻿using AutoMapper;
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
    [Route("api/request")]
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

        [HttpGet("GetNewRequest", Name = "NewRequest")]
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
        /// Request Details
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>

        [HttpGet("GetRequestDetails", Name = "RequestDetails")]
        public async Task<IActionResult> GetDetails(int requestId)
        {
            var noexiste = await context.Requests.AnyAsync(x => x.RequestId == requestId);

            if (!noexiste)
            {
                return NotFound("No existe esa solicitud");
            }

            var company = await context.Requests
                .Include(requestDB => requestDB.Company).Select(y => y.CompanyId).FirstOrDefaultAsync();

            if (context.Companies.Where(x => x.CompanyId == company)
               .Select(x => x.CompanyType).FirstOrDefault())
            {

                var requestSeller = await context.Requests.Include(requestDB => requestDB.Quotes)
                    .FirstOrDefaultAsync(x => x.RequestId == requestId);

                context.Entry(requestSeller).Reference(x => x.Address).Load();


                return Ok(mapper.Map<RequestDetailSellerDTO>(requestSeller));
            }
            else
            {
                var requestBuyer = await context.Requests.Include(requestDB => requestDB.Quotes)
                .FirstOrDefaultAsync(x => x.RequestId == requestId);

                context.Entry(requestBuyer).Reference(x => x.Address).Load();


                return Ok(mapper.Map<RequestDetailBuyerDTO>(requestBuyer));
            }

            
        }

        /// <summary>
        /// Get All Requests
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>

        [HttpGet("GetAllRequests", Name = "AllRequests")]
        public async Task<IActionResult> GetAllRequests(int companyId)
        {
            var existe = await context.Companies.AnyAsync(x => x.CompanyId == companyId);

            if (!existe)
            {
                return NotFound("La compañia no existe en el sistema");
            }


            if (context.Companies.Where(x => x.CompanyId == companyId)
                .Select(x => x.CompanyType).FirstOrDefault())
            {

                var request = await context.Requests.ToListAsync();

                return Ok(mapper.Map<List<RequestSellerDTO>>(request));
            }
            else
            {
                var requests = await context.Requests.Include(x => x.Company)
               .Where(requestDB => requestDB.CompanyId == companyId).ToListAsync();

                var result = mapper.Map<List<RequestBuyerDTO>>(requests);

                int qty = 0;

                foreach (var req in result)
                {
                    qty = await context.QuoteRequests.Where(x => x.RequestId == req.RequestId).CountAsync();
                    req.QuotesQty = qty;
                    qty = 0;
                }

                return Ok(result);
            }
               
        }



        }
}