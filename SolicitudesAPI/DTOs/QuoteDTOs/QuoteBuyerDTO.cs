namespace SolicitudesAPI.DTOs.QuoteDTOs
{
    public class QuoteBuyerDTO
    {
        public int QuoteId { get; set; }
        public string QuoteProductName { get; set; }
        public int DeliveryDeadLineInDays { get; set; }
        public DateTime QuoteExpirationDate { get; set; }
        public decimal NetCost { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public string WebSiteUrl { get; set; }
        public bool IsProductExactMatch { get; set; }

    }
}
