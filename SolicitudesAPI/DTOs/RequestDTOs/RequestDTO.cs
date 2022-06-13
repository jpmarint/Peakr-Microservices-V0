using SolicitudesAPI.DTOs.QuoteDTOs;

namespace SolicitudesAPI.DTOs.RequestDTOs
{
    public class RequestDTO
    {
        public int RequestId { get; set; }
        public string QuerySearch { get; set; }
        public DateTime RequestDate { get; set; }
        public int Quantity { get; set; }
        public string StatusRequest { get; set; }
        public string SKU { get; set; }
        public string CompanyName { get; set; }
        public string LogoPath { get; set; }
        public string WebSiteUrl { get; set; }
        public int QuotesQty { get; set; }


    }
}
