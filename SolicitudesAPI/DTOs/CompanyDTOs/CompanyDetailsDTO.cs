namespace SolicitudesAPI.DTOs.CompanyDTOs
{
    public class CompanyDetailsDTO
    {
        public int CompanyId { get; set; }
        public int CompanyType { get; set; }       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime? EstablishedSince { get; set; }
        public string NIT { get; set; }
        public string WebSiteUrl { get; set; }
        public string TotalEmployees { get; set; }
        public string YearlySalesVolume { get; set; }


    }
}
