using SolicitudesAPI.DTOs.QuoteDTOs;

namespace SolicitudesAPI.DTOs.RequestDTOs
{
    public class RequestDetailBuyerDTO
    {

        public int RequestId { get; set; }
        public string QuerySearch { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? Quantity { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string PaymentConditions { get; set; }
        public string ProductNeeds { get; set; }
        public List<QuoteBuyerDTO> Quotes { get; set; }

    }
}
