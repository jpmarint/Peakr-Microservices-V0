using SolicitudesAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.DTOs
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
        [Range(0, 1)]
        public int CompanyType { get; set; }
        [Required]
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 6)]
        [TipoArchivoValidacion(GrupoTipoArchivo.File)]
        public IFormFile LegalExistenceDocPath { get; set; }
        [Required]
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 6)]
        [TipoArchivoValidacion(GrupoTipoArchivo.File)]
        public IFormFile BankAccountDocPath { get; set; }
        [Required]
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 6)]
        [TipoArchivoValidacion(GrupoTipoArchivo.File)]
        public IFormFile RutDocPath { get; set; }

        //public int AddressId { get; set; }


    }
}
