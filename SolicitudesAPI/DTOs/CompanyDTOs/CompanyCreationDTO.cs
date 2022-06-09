using SolicitudesAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.DTOs.CompanyDTOs
{
    public class CompanyCreationDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Nit { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]       
        public bool CompanyType { get; set; }
        [Required]
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 6)]
        [TipoArchivoValidacion(GrupoTipoArchivo.File)]
        public IFormFile LegalExistenceDoc { get; set; }
        [Required]
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 6)]
        [TipoArchivoValidacion(GrupoTipoArchivo.File)]
        public IFormFile BankAccountDoc { get; set; }
        [Required]
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 6)]
        [TipoArchivoValidacion(GrupoTipoArchivo.File)]
        public IFormFile RutDoc { get; set; }

        //public int AddressId { get; set; }


    }
}
