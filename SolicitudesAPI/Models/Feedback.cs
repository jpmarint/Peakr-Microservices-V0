using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string? Rating { get; set; }
        public string? FeedbackType { get; set; }
        public string? Comment { get; set; }
        public string? Email { get; set; }
        public string? Url { get; set; }
    }
}
