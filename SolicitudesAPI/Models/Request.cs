using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudesAPI.Models
{
    public class Request
    {

        public Request()
        {
            Quotes = new HashSet<Quote>();
            Categories = new HashSet<RequestCategory>();

        }

        [Key]
        [Required]
        public int RequestId { get; set; }

        public string statusRequest { get; set; } = "Por cotizar";
        [Required]
        public string QuerySearch { get; set; }

        public string? SKU { get; set; }

        public bool IsExactProduct { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address? Address { get; set; } 

        [Required]
        public string PaymentConditions { get; set; }


        [Required]
        public int Quantity { get; set; }


        public string? ProductNeeds { get; set; } = string.Empty;

        public int? ChosenQuote { get; set; }

        public string? FileGuid { get; set; } = string.Empty;
        public string? FileKey { get; set; } = string.Empty;

        [NotMapped]
        public List<SelectListItem> ProductCategories { get; set; }

        [NotMapped]
        public List<int> SelectedCategories { get; set; }


        [NotMapped]
        public string CompanyLogo { get; set; }


        /*************************************************************************
         * Navigation properties
         *************************************************************************/

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Quote> Quotes { get; set; }

        public virtual ICollection<RequestCategory> Categories { get; private set; }
    }
}
