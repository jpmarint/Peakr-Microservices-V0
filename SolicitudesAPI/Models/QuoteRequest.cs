using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudesAPI.Models
{
    public class QuoteRequest
    {
        public QuoteRequest(int requestId, int quoteId, int workflowStatusId = 0)
        {
            this.RequestId = requestId;
            this.QuoteId = quoteId;
            this.WorkflowStatusId = workflowStatusId;
        }

        [Key]
        public int QuoteRequestId { get; set; }
        public bool IsPurchaseOrder { get; set; } = false;
        public bool IsCancelled { get; set; } = false;

        [ForeignKey("Quote")]
        public int QuoteId;
        public Quote Quote { get; set; }

        [ForeignKey("Request")]
        public int RequestId;
        public virtual Request Request { get; set; }

        [ForeignKey("QuoteRequestStatus")]
        public int WorkflowStatusId;
        public WorkflowStatus QuoteRequestStatus { get; set; } = null!;

        public PurchaseOrder PurchaseOrder { get; set; }

    }
}
