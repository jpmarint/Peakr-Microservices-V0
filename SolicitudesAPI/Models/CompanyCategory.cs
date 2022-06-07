namespace SolicitudesAPI.Models
{
    public class CompanyCategory
    {

        public int CompanyId { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Company Company { get; set; }
    }
}
