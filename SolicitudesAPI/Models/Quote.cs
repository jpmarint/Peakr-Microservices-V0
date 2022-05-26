using System;
using System.Collections.Generic;

namespace SolicitudesAPI.Models
{
    public partial class Quote
    {
        public int QuoteId { get; set; }
        public int? DeliveryDeadLineInDays { get; set; }
        public bool? IsProductExactMatch { get; set; }
        public decimal? NetCost { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? TotalGrossPrice { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? ServiceCost { get; set; }
        public decimal? TaxHold { get; set; }
        public decimal? SellerIncome { get; set; }
        public DateTime? QuoteExpirationDate { get; set; }
        public string QuoteNotes { get; set; }
        public string QuoteProductName { get; set; }
        public int? CompanyId { get; set; }
        public int? RequestId { get; set; }

        public virtual Request Request { get; set; }
    }
}
