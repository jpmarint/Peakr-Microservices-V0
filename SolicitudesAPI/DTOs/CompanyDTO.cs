using SolicitudesAPI.Validaciones;

namespace SolicitudesAPI.DTOs
{
    public class CompanyDTO
    {
        public string CompanyName { get; set; }
       
        public string LogoPath { get; set; }
        public string WebSiteUrl { get; set; }
        public List<RequestDTO> Requests { get; set; }

    }
}
