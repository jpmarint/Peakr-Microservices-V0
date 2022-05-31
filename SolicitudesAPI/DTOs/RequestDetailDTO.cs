namespace SolicitudesAPI.DTOs
{
    public class RequestDetailDTO
    {

        public int RequestId { get; set; }
        public string QuerySearch { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? Quantity { get; set; }
        public string City { get; set; }
        public string PaymentConditions { get; set; }
        public string ProductNeeds { get; set; }
        public List<QuoteDTO> Quotes { get; set; }
    }
}
