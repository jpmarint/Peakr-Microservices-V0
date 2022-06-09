using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class CompanyCategory
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Company Company { get; set; }
    }
}
