namespace SolicitudesAPI.DTOs
{
    public class QuoteDTO
    {
        public int QuoteId { get; set; }
        public string QuoteProductName { get; set; }
        public int? DeliveryDeadLineInDays { get; set; }
        public DateTime? QuoteExpirationDate { get; set; }
        public decimal? PricePerUnit { get; set; }

    }
}
