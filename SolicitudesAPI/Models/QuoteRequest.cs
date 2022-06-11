using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudesAPI.Models
{
    public class QuoteRequest
    {
       
        public bool IsPurchaseOrder { get; set; } = false;
        public bool IsCancelled { get; set; } = false;

        public int QuoteId;
        public Quote Quote { get; set; }

        public int RequestId;
        public virtual Request Request { get; set; }
    }
}
