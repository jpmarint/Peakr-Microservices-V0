namespace SolicitudesAPI.DTOs.RequestDTOs
{
    public class RequestModalDTO
    {
        public int RequestId { get; set; }
        public string QuerySearch { get; set; }
        public bool IsExactProduct { get; set; }
        public DateTime RequestDate { get; set; }
        public int Quantity { get; set; }
        public string PaymentConditions { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public string ProductNeeds { get; set; }
        public string deliveryInstructions { get; set; }
        public string FilePath { get; set; }
        public int? AddressId { get; set; }
    }
}
