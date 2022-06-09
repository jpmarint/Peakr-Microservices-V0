using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudesAPI.Models
{
    public class DiscardedRequest
    {
        [Key]
        public int DiscardedRequestId { get; set; }

        /*************************************************************************
         * Navigation properties
         *************************************************************************/
        [ForeignKey("Company")]
        public int CompanyId;
        public virtual Company Company { get; set; }


        [ForeignKey("Request")]
        public int RequestId;
        public virtual Request Request { get; set; }
    }
}
