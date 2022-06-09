using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class Category
    {

        [Key]
        [Required]
        public int CategoryId { get; set; }

        public int CategoryCode { get; set; }
       
        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
