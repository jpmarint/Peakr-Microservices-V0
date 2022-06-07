using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class CompanyCategory
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Company Company { get; set; }
    }
}
