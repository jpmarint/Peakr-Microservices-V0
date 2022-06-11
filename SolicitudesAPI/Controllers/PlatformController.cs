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
        /// Get File
        /// </summary>
        /// <param name="companyCreationDTO"></param>
        /// <returns></returns>
        /// 
        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(int companyId,
            string fileKey)
        {

            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }

            var companyRecord = await context.Companies
                       .Where(x => x.CompanyId == companyId).FirstOrDefaultAsync();

            fileKey = fileKey.Trim().Replace(" ", String.Empty).ToLowerInvariant();

            var response = new FileDTO();

            switch (fileKey)
            {
                case "logo":
                   
                    response.FilePath = almacenadorArchivos.GenerateSASTokenForFile(companyRecord.LogoGuid);
                    response.FileName = companyRecord.LogoKey;
                    break;
                case "banner":
                    response.FilePath = almacenadorArchivos.GenerateSASTokenForFile(companyRecord.ImageGuid);
                    response.FileName = companyRecord.ImageKey;
                    break;
                case "rut":
                    response.FilePath = almacenadorArchivos.GenerateSASTokenForFile(companyRecord.RutDocGuid);
                    response.FileName = companyRecord.RutDocKey;
                    break;
                case "exist":
                    response.FilePath = almacenadorArchivos.GenerateSASTokenForFile(companyRecord.LegalExistenceDocGuid);
                    response.FileName = companyRecord.LegalExistenceDocKey;
                    break;
                case "bank":
                    response.FilePath = almacenadorArchivos.GenerateSASTokenForFile(companyRecord.BankAccountDocGuid);
                    response.FileName = companyRecord.BankAccountDocKey;
                    break;

                default:

                    return BadRequest("El archivo que deseas cambiar no existe");

            }

            return Ok(response);

        }

        /// <summary>
        /// Get Address
        /// </summary>
        /// <param name="companyCreationDTO"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetAddress")]
        public async Task<IActionResult> GetAddress(int addressId)
        {
            var exist = await context.Address.AnyAsync(x => x.AddressId == addressId);
            if (!exist)
            {
                return NotFound("No existe esta dirección");
            }

            var addressRecord = await context.Address.FirstOrDefaultAsync(x => x.AddressId == addressId);

            return Ok(mapper.Map<AddressDTO>(addressRecord));


        }

            

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <param name="companyCreationDTO"></param>
        /// <returns></returns>
        /// 
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {

            var categoriesList = await context.Categories.ToListAsync();

            return Ok(mapper.Map<List<CategoryDTO>>(categoriesList));

        }

        /// <summary>
        /// Create Address
        /// </summary>
        /// <param name="companyCreationDTO"></param>
        /// <returns></returns>
        /// 

        [HttpGet("CreateAddress")]
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

        [HttpPut("UpsertFile")]
        public async Task<IActionResult> UpsertFile(int companyId,
           string fileKey, string fileGuid)
        {

            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }

            if (fileKey != null)
            {
                string newPath = string.Empty;
                newPath = fileGuid;
                var companyRecord = await context.Companies
                        .Where(x => x.CompanyId == companyId).FirstOrDefaultAsync();

                fileKey = fileKey.Trim().Replace(" ", String.Empty).ToLowerInvariant();
                switch (fileKey)
                {
                    case "logo":
                        companyRecord.LogoGuid = newPath;
                        break;
                    case "banner":
                        companyRecord.ImageGuid = newPath;
                        break;
                    case "rut":
                        companyRecord.RutDocGuid = newPath;
                        break;
                    case "exist":
                        companyRecord.LegalExistenceDocGuid = newPath;
                        break;
                    case "bank":
                        companyRecord.BankAccountDocGuid = newPath;
                        break;

                    default:

                        return BadRequest("El archivo que deseas cambiar no existe");

                }

                context.Update(companyRecord);
                await context.SaveChangesAsync();
                return Ok("Se ha actualizado el archivo correctamente");
            }
            else
            {
                return BadRequest("Debe subir un archivo");
            }
        }

        /// <summary>
        /// Update Address
        /// </summary>
        /// <param name="companyCreationDTO"></param>
        /// <returns></returns>
        /// 
        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(int addressId, AddressDTO addressDTO)
        {
            var exist = await context.Address.AnyAsync(x => x.AddressId == addressId);
            if (!exist)
            {
                return NotFound("No existe esta dirección");
            }

            var addressRecord = await context.Address.FirstOrDefaultAsync(x => x.AddressId == addressId);

            addressRecord.Line1 = addressDTO.Line1;
            addressRecord.Line2 = addressDTO.Line2;
            addressRecord.PostalCode = addressDTO.PostalCode;
            addressRecord.Department = addressDTO.Department;
            addressRecord.City = addressDTO.City;
            addressRecord.Notes = addressDTO.Notes;

            context.Update(addressRecord);
            await context.SaveChangesAsync();

            return Ok("Se actualizó la dirección");
        }

    }
}
