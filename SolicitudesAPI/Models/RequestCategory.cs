using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class RequestCategory
    {
        [Key]
        public int Id { get; set; }

        public int RequestId { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Request Request { get; set; }

    }
}
