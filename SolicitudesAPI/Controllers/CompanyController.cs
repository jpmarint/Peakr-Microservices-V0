using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.Models;
using SolicitudesAPI.Servicios;

namespace SolicitudesAPI.Controllers
{
    [ApiController]
    [Route("api/companies")]
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
       
        [HttpPost(Name = "RegisterCompany")]
        public async Task<IActionResult> Post([FromForm] CompanyCreationDTO companyCreationDTO)
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
                    company.LegalExistenceDocPath = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
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
                    company.BankAccountDocPath = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
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
                    company.RutDocPath = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
                    companyCreationDTO.RutDocPath.ContentType, companyName, RutFileName);

                }
            }
            context.Add(company);
            await context.SaveChangesAsync();
            var dto = mapper.Map<CompanyDTO>(company);
            return Ok(company);

        }

       
    }
}
