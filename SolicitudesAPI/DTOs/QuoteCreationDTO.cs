namespace SolicitudesAPI.DTOs
{
    public class QuoteCreationDTO
    {
        public string QuoteProductName { get; set; }
        public int DeliveryDeadLineInDays { get; set; }
        public DateTime QuoteExpirationDate { get; set; }
        public decimal Iva { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalGrossPrice { get; set; }
        public decimal TotalIVA { get; set; }
        public decimal NetCost { get; set; }
        public decimal TaxWithholding { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal SellerIncome { get; set; }
        public bool IsProductExactMatch { get; set; }
        public string NotesToClient { get; set; }
        public int CompanyId { get; set; }
        public List<int> RequestId { get; set; }
        


    }
}
