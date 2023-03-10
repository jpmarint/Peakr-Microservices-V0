using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    /// <summary>
    /// Permite registrar las categorías de los productos
    /// que vende la empresa
    /// </summary>
    public class Address
    {
        /// <summary>
        /// hola
        /// </summary>
        [Required]
        [Key]
        public int AddressId { get; set; }

        //Address
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }

        [StringLength(maximumLength: 10, ErrorMessage = "No debe tener mas de {1} caracteres")]
        public string? PostalCode { get; set; }
        public string? Department { get; set; }
        public string? City { get; set; }
        public string? Notes { get; set; }


    }


}

