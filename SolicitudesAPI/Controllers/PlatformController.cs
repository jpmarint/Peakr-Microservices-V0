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

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, int companyId)
        {
            var companyName = await context.Companies
                        .Where(companyDB => companyDB.CompanyId == companyId)
                       .Select(x => x.Name).FirstOrDefaultAsync();
            var filePath = await almacenadorArchivos.UploadFileToBlob(companyName, file);

            return Ok(filePath);

        }

        [HttpDelete("DeleteFile")]
        public async Task<IActionResult> DeleteFile(string filePath, int companyId)
        {
            var companyName = await context.Companies
                        .Where(companyDB => companyDB.CompanyId == companyId)
                       .Select(x => x.Name).FirstOrDefaultAsync();
            await almacenadorArchivos.BorrarArchivo(filePath, contenedor, companyName);

            return Ok("Archivo borrado");
        }
    }
}
