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
        public PlatformController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        //endpoint para guardar "documento para ilustrar" en newrequest
        //[HttpPost("UploadFile")]
        //public async Task<IActionResult> UploadDocument(IFormFile file, int origin, int companyId)
        //{

        //    if (file != null)
        //    {
                
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            var fileDto = new FileDTO();
        //            await file.CopyToAsync(memoryStream);
        //            var contenido = memoryStream.ToArray();
        //            var extension = Path.GetExtension(file.FileName);
        //            var companyName = await context.Companies
        //                .Where(companyDB => companyDB.CompanyId == companyId)
        //                .Select(x => x.Name).FirstOrDefaultAsync();
        //            var fileName = file.FileName;
        //            var contenedor = "";
        //            switch (origin)
        //            {
        //                case 0: // 0 => Request Creation
        //                   contenedor = $"documentos/{companyName}/requests";
        //                   fileDto.result = $"Almacenado en {contenedor}";
        //                    break;
        //                case 1:  // 1 => Quote Creation
        //                   contenedor = $"documentos/{companyName}/quotes";
        //                   fileDto.result = $"Almacenado en {contenedor}";
        //                    break;
        //                case 2:  // 2 => Company Profile Documents Update
        //                   contenedor = $"documentos/{companyName}";
        //                   fileDto.result = $"Almacenado en {contenedor}";
        //                    break;
        //                default:
        //                    return BadRequest("Origen no existe");                     
        //            } 
        //            var filePath = await almacenadorArchivos
        //                .GuardarArchivoRequest(contenido, extension, contenedor,
        //            file.ContentType, fileName);

        //            fileDto.FilePath = filePath;
        //            fileDto.FileName = fileName;

        //            return ((IActionResult)fileDto);
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("debe cargar un documento");
        //    }
        //}
    }
}
