using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class Request
    {

        [Key]
        [Required]
        public int RequestId { get; set; }
        [Required]
        public string QuerySearch { get; set; }

        public string? SKU { get; set; }

        public bool IsExactProduct { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public string PaymentConditions { get; set; }


        [Required]
        public int Quantity { get; set; }

        [Required]
        public string ProductNeeds { get; set; }

        public int? ChosenQuote { get; set; }

        public string deliveryInstructions { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Companies { get; set; }

        public string StatusRequest { get; set; }

        public string? FilePath { get; set; } = string.Empty;

        public string? FileName { get; set; }

        public int AddressId { get; set; }
        public Address? Address { get; set; }

        public List<QuoteRequest> QuoteRequest { get; set; }

        public List<RequestCategory> requestCategories { get; set; }
    }
}
