namespace SolicitudesAPI.Models
{
    public class RequestCategory
    {

        public int RequestId { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Request Request { get; set; }

    }
}
