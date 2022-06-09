using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudesAPI.Models
{
    public class Quote
    {
        [Key]
        [Required]
        public int QuoteId { get; set; }

        [Required]
        public string QuoteProductName { get; set; }

        [Required]
        public int DeliveryDeadLineInDays { get; set; }

        public string? FilePath { get; set; } = string.Empty;

        [Required]
        public DateTime QuoteExpirationDate { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal PricePerUnit { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal TotalGrossPrice { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal IVA { get; set; }


        [Required]
        [Precision(18, 2)]
        public decimal TotalIVA { get; set; }


        [Required]
        [Precision(18, 2)]
        public decimal NetCost { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal TaxWithholding { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal ServiceCost { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal SellerIncome { get; set; }


        public string? NotesToClient { get; set; }

        [Required]
        public string PaymentConditions { get; set; }

        [Required]
        public bool IsProductExactMatch { get; set; }

        [Required]
        public int State { get; set; } = 0;

        /*************************************************************************
         * Navigation properties
         *************************************************************************/
        [ForeignKey("Company")]
        public int CompanyId;
        public virtual Company Company { get; set; }


        [ForeignKey("Request")]
        public int RequestId;
        public virtual Request Request { get; set; }

        public QuoteRequest QuoteRequest { get; set; }
    }
}
