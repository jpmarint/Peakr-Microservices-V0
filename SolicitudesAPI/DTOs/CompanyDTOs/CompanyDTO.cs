using SolicitudesAPI.DTOs.RequestDTOs;
using SolicitudesAPI.Validaciones;

namespace SolicitudesAPI.DTOs.CompanyDTOs
{
    public class CompanyDTO
    {
        public string Name { get; set; }
        public string WebSiteUrl { get; set; }
        public List<RequestDTO> Requests { get; set; }

    }
}
