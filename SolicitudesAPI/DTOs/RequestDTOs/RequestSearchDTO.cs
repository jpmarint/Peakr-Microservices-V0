namespace SolicitudesAPI.DTOs.RequestDTOs
{
    public class RequestSearchDTO
    {
        public string QuerySearch { get; set; }
        public DateTime RequestDate { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PostalCode { get; set; }
        public string Notes { get; set; }

    }
}
