using SolicitudesAPI.DTOs.QuoteDTOs;
using SolicitudesAPI.Models;

namespace SolicitudesAPI.DTOs.RequestDTOs
{
    public class RequestBuyerDTO
    {
        public int RequestId { get; set; }
        public string QuerySearch { get; set; }
        public DateTime? RequestDate { get; set; }
        public string StatusRequest { get; set; }
        public string SKU { get; set; }
        public int QuotesQty { get; set; }

    }
}
