using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudesAPI.Models
{
    public class RequestCategory
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Request")]
        public int RequestId;
        public virtual Request Request { get; set; }

        [ForeignKey("Category")]
        public int CategoryId;
        public virtual Category Category { get; set; }
    }
}
