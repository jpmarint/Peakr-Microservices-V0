namespace SolicitudesAPI.DTOs.RequestDTOs
{
    public class RequestModalDTO
    {

        public string QuerySearch { get; set; }
        public bool IsExactProduct { get; set; }
        public string StatusRequest { get; set; }
        public DateTime RequestDate { get; set; }
        public int Quantity { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string Line2 { get; set; }
        public string PostalCode { get; set; }
        public string PaymentConditions { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public string WebSiteUrl { get; set; }
        public string ProductNeeds { get; set; }
        public string deliveryInstructions { get; set; }
    }
}
