using SolicitudesAPI.Validaciones;

namespace SolicitudesAPI.DTOs
{
    public class CompanyCreationDTO
    {
        public string CompanyName { get; set; }
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 6)]
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        public IFormFile LogoPath { get; set; }
        public string WebSiteUrl { get; set; }
        public int? Nit { get; set; }
        public string CompanyDescription { get; set; }

    }
}
