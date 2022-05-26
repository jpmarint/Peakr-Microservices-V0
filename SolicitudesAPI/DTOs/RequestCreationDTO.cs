namespace SolicitudesAPI.DTOs
{
    public class RequestCreationDTO
    {

        public string ProductName { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? Quantity { get; set; }
        public string RequestStatus { get; set; }
        public int? CompanyId { get; set; }
        public string PaymentConditions { get; set; }
        public string QuerySearch { get; set; }
        public int? AdressId { get; set; }
   

    }
}
