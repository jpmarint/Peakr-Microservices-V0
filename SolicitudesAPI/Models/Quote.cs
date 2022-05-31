using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string NotesToClient { get; set; }


        [Required]
        public bool IsProductExactMatch { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public List<QuoteRequest> QuoteRequests { get; set; }
    }
}
