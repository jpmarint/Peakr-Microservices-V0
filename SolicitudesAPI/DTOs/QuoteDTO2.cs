namespace SolicitudesAPI.DTOs
{
    public class QuoteDTO2
    {
        public string QuoteProductName { get; set; }
        public int DeliveryDeadLineInDays { get; set; }
        public DateTime QuoteExpirationDate { get; set; }
        public decimal IVA { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalGrossPrice { get; set; }
        public decimal TotalIVA { get; set; }
        public decimal NetCost { get; set; }
        public decimal TaxWithholding { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal SellerIncome { get; set; }
        public bool IsProductExactMatch { get; set; }
        public string NotesToClient { get; set; }

    }
}
