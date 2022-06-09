using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class WorkflowStatus
    {
        [Key]
        [Required]
        public int StatusId { get; set; }

        [Required]
        public int StatusCode { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string cssStyleName { get; set; }
    }
}
