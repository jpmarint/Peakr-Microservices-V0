namespace SolicitudesAPI.DTOs
{
    public class RequestDTO
    {
        public int RequestId { get; set; }
        public string ProductName { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? Quantity { get; set; }
        public string RequestStatus { get; set; }
        public string CompanyName { get; set; }
        public string LogoPath { get; set; }
        public string WebSiteUrl { get; set; }

    }
}
