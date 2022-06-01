using AutoMapper;
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

            var company = mapper.Map<Company>(companyCreationDTO);

            if (companyCreationDTO.LegalExistenceDocPath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.LegalExistenceDocPath.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(companyCreationDTO.LegalExistenceDocPath.FileName);
                    company.LegalExistenceDocPath = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
                    companyCreationDTO.LegalExistenceDocPath.ContentType);

                }
            }

            if (companyCreationDTO.BankAccountDocPath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.BankAccountDocPath.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(companyCreationDTO.BankAccountDocPath.FileName);
                    company.BankAccountDocPath = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
                    companyCreationDTO.BankAccountDocPath.ContentType);

                }
            }

            if (companyCreationDTO.RutDocPath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.RutDocPath.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(companyCreationDTO.RutDocPath.FileName);
                    company.RutDocPath = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
                    companyCreationDTO.RutDocPath.ContentType);

                }
            }
            context.Add(company);
            await context.SaveChangesAsync();
            var dto = mapper.Map<CompanyDTO>(company);
            return Ok();

        }

       
    }
}
