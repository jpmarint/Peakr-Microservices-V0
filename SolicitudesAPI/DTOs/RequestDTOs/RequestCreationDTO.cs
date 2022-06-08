using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.DTOs.RequestDTOs
{
    public class RequestCreationDTO
    {

        public string QuerySearch { get; set; }

        public string SKU { get; set; }

        public bool IsExactProduct { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public string PaymentConditions { get; set; }


        [Required]
        public int Quantity { get; set; }

        [Required]
        public string ProductNeeds { get; set; }

        public IFormFile FilePath { get; set; }

        public string FileName { get; set; }

        //public int? ChosenQuote { get; set; }

        public string deliveryInstructions { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string StatusRequest { get; set; }
        [Required]
        public int AddressId { get; set; }

        public List<int> CategoriesIds { get; set; }
    }
}
