using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class ExceptionLog
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
