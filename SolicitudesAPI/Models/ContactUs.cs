using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class ContactUs
    {
        [Key]
        [Required]
        public int ContactRequestId { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El mensaje es requerido")]
        public string Message { get; set; }
        public bool IsNewMessage { get; set; } = true;
    }
}

