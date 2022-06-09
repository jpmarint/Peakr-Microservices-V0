using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudesAPI.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }
        public bool IsPaid { get; set; } = false;
        [Required]

        public DateTime CreationDate { get; set; }
        public DateTime PaidDate { get; set; }

        public string? Notes { get; set; } = String.Empty;

        [ForeignKey("QuoteRequest")]
        public int QuoteRequestId;
        public virtual QuoteRequest QuoteRequest { get; set; } = null!;

        [ForeignKey("CompanySeller")]
        public int CompanySellerId;
        public Company CompanySeller { get; set; } = null!;

        [ForeignKey("CompanyBuyer")]
        public int CompanyBuyerId;

        public Company CompanyBuyer { get; set; } = null!;
    }
}
