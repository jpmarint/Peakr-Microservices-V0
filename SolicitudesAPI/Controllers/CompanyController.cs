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
        private readonly SolicitudesAPIContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "logos";
        public CompanyController(SolicitudesAPIContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CompanyCreationDTO companyCreationDTO)
        {

            var company = mapper.Map<Company>(companyCreationDTO);

            if (companyCreationDTO.LogoPath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.LogoPath.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(companyCreationDTO.LogoPath.FileName);
                    company.LogoPath = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
                        companyCreationDTO.LogoPath.ContentType);

                }
            }

            context.Add(company);
            await context.SaveChangesAsync();
            var dto = mapper.Map<CompanyDTO>(company);
            return Ok();

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompanyDTO>> Get(int id)
        {
            var existe = await context.Companies.AnyAsync(x => x.CompanyId == id);

            if (!existe)
            {
                return NotFound("La compañia no existe en el sistema");
            }

            var company = await context.Companies.Include(companyBD => companyBD.Requests).FirstOrDefaultAsync(x => x.CompanyId == id);
            return mapper.Map<CompanyDTO>(company);
        }
    }
}
