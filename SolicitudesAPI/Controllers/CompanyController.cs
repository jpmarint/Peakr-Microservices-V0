using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.DTOs.CompanyDTOs;
using SolicitudesAPI.Models;
using SolicitudesAPI.Servicios;

namespace SolicitudesAPI.Controllers
{
    [ApiController]
    [Route("Api/Companies")]
    public class CompanyController : ControllerBase
    {
        private readonly string RutFileName = "RUT";
        private readonly string LegalExistenceFileName = "Cert_Existencia";
        private readonly string BankAccountFileName = "Cert_Bancario";
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "documentos";
        public CompanyController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        /// <summary>
        /// Register Company
        /// </summary>
        /// <param name="companyCreationDTO"></param>
        /// <returns></returns>
       
        [HttpPost("Register", Name = "RegisterCompany")]
        public async Task<IActionResult> RegisterCompany([FromForm] CompanyCreationDTO companyCreationDTO)
        {

            var ExisteCompanymismocorreo = await context.Companies.AnyAsync(x => x.Email == companyCreationDTO.Email);

            if (ExisteCompanymismocorreo)
            {
                return BadRequest($"Ya existe una compañia con el correo {companyCreationDTO.Email}, inicie sesión o registre una " +
                    $"empresa diferente, en caso de no estar registrado en Peakr, contactar con support@peakr.app.");
            }

            var ExisteCompanymismoNIT = await context.Companies.AnyAsync(x => x.NIT == companyCreationDTO.Nit);

            if (ExisteCompanymismoNIT)
            {
                return BadRequest($"Ya existe una compañia con el NIT {companyCreationDTO.Nit}, inicie sesión o registre una " +
                    $"empresa diferente, en caso de no estar registrado en Peakr, contactar con support@peakr.app.");
            }

            var ExisteCompanymismoNombre = await context.Companies.AnyAsync(x => x.Name == companyCreationDTO.Name);

            if (ExisteCompanymismoNombre)
            {
                return BadRequest($"Ya existe una compañia con el nombre {companyCreationDTO.Name}, inicie sesión o registre una " +
                    $"empresa diferente, en caso de no estar registrado en Peakr, contactar con support@peakr.app.");
            }

            var company = mapper.Map<Company>(companyCreationDTO);

            if (companyCreationDTO.LegalExistenceDocPath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.LegalExistenceDocPath.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(companyCreationDTO.LegalExistenceDocPath.FileName);
                    var companyName = companyCreationDTO.Name;
                    company.LegalExistenceDocPath = await almacenadorArchivos.GuardarArchivoCompany(contenido, extension, contenedor,
                    companyCreationDTO.LegalExistenceDocPath.ContentType, companyName, LegalExistenceFileName);

                }
            }

            if (companyCreationDTO.BankAccountDocPath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.BankAccountDocPath.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var companyName = companyCreationDTO.Name;
                    var extension = Path.GetExtension(companyCreationDTO.BankAccountDocPath.FileName);
                    company.BankAccountDocPath = await almacenadorArchivos.GuardarArchivoCompany(contenido, extension, contenedor,
                    companyCreationDTO.BankAccountDocPath.ContentType, companyName, BankAccountFileName);

                }
            }

            if (companyCreationDTO.RutDocPath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.RutDocPath.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var companyName = companyCreationDTO.Name;
                    var extension = Path.GetExtension(companyCreationDTO.RutDocPath.FileName);
                    company.RutDocPath = await almacenadorArchivos.GuardarArchivoCompany(contenido, extension, contenedor,
                    companyCreationDTO.RutDocPath.ContentType, companyName, RutFileName);

                }
            }
            context.Add(company);
            await context.SaveChangesAsync();
            var dto = mapper.Map<CompanyDTO>(company);
            return Ok(company);

        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetCompanyDetails(int companyId)
        {
            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }

            var company = await context.Companies.FirstOrDefaultAsync(x => x.CompanyId == companyId);
            
            var companyDetails = mapper.Map<CompanyDetailsDTO>(company);

            return Ok(companyDetails);
        }

        [HttpGet("Address")]
        public async Task<IActionResult> GetCompanyAddress(int companyId)
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
            return Ok(mapper.Map<AddressDTO>(address));
        }

        //[HttpGet("Docs")]
        //public async Task<IActionResult> GetCompanyDocs(int companyId)
        //{
        //    var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
        //    if (!exist)
        //    {
        //        return NotFound("No existe esta compañía");
        //    }

        //    var company = await context.Companies.FirstOrDefaultAsync(x => x.CompanyId == companyId);

        //    var companyDocs = mapper.Map<CompanyDocsDTO>(company);

        //    return Ok(companyDocs);
        //}

        //[HttpPost("UpdateDetails")]
        //public async Task<IActionResult> UpdateCompanyDetails(CompanyDetailsDTO companyDetails)
        //{
        //    var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyDetails.CompanyId);
        //    if (!exist)
        //    {
        //        return NotFound("No existe esta compañía");
        //    }

            
            
        //    return Ok("Los detalles de la compañía se actualizaron.");
        //}


    }
}
