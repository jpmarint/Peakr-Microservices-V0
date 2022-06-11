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

    }
}
