using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.Models;
using SolicitudesAPI.Servicios;

namespace SolicitudesAPI.Controllers
{
    [ApiController]
    [Route("Api/Platform")]
    public class PlatformController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "documentos";
        public PlatformController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }


        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="companyId"></param>
        /// /// <param name="file"></param>
        /// <returns></returns>

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, int companyId)
        {
            var companyName = await context.Companies
                        .Where(companyDB => companyDB.CompanyId == companyId)
                       .Select(x => x.Name).FirstOrDefaultAsync();
            var filePath = await almacenadorArchivos.UploadFileToBlob(companyName, file);

            return Ok(filePath);

        }

        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDTO>> GetAddress(int addressId)
        {
            var exist = await context.Address.AnyAsync(x => x.AddressId == addressId);
            if (!exist)
            {
                return NotFound("No existe esta dirección");
            }

            var address = await context.Address
                .Where(addressDB => addressDB.AddressId == addressId).FirstOrDefaultAsync();

            return mapper.Map<AddressDTO>(address);

        }

        [HttpPost("CreateAddress")]
        public async Task<IActionResult> CreateAddress(AddressDTO newAddress)
        {          
            var address = new Address();

            address.Line1 = newAddress.Line1;
            address.Line2 = newAddress.Line2;
            address.PostalCode = newAddress.PostalCode;
            address.Department = newAddress.Department;
            address.City = newAddress.City;
            address.Notes = newAddress.Notes;

            context.Address.Add(address);
            context.SaveChanges();
            return Ok(address.AddressId);
        }

    }
}
