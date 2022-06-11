using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.DTOs.CompanyDTOs
{
    public class CompanyDetailsDTO
    {
        public int CompanyId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstablishedSince { get; set; }
        [Required]
        public string NIT { get; set; }
        public string WebSiteUrl { get; set; }
        public string TotalEmployees { get; set; }
        public string YearlySalesVolume { get; set; }
        public bool CompanyType { get; set; }
        public int? AddressId { get; set; }
    }
}
