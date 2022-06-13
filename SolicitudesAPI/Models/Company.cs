using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitudesAPI.Models
{
    public class Company
    {
        public Company()
        {
            Categories = new HashSet<CompanyCategory>();
            Requests = new HashSet<Request>();
            Quotes = new HashSet<Quote>();
            PurchaseOrdersSells = new HashSet<PurchaseOrder>();
            PurchaseOrdersBuys = new HashSet<PurchaseOrder>();
        }
        public int CompanyId { get; set; }
        [Required]
        public bool CompanyType { get; set; }
        public string? ImageKey { get; set; } = "Banner";
        public string? ImageGuid { get; set; } = "https://peakrweb.blob.core.windows.net/logos/BannerTemp.png";
        public string? LogoKey { get; set; } = "Logo";
        public string? LogoGuid { get; set; } = "https://peakrweb.blob.core.windows.net/logos/LogoTemp.png";

        [Required(ErrorMessage = "El nombre de la empresa es requerido")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        public string? Email { get; set; }
        public string? Description { get; set; }
        public DateTime? EstablishedSince { get; set; }

        [Required(ErrorMessage = "El NIT es requerido")]
        public string? NIT { get; set; }
        public string? WebSiteUrl { get; set; }
        public string? TotalEmployees { get; set; }
        public string? YearlySalesVolume { get; set; }

        //Docs
        public string? LegalExistenceDocGuid { get; set; } 
        public string LegalExistenceDocKey { get; set; } = "Cert_Existencia";
        public string? BankAccountDocGuid { get; set; } 
        public string? BankAccountDocKey { get; set; } = "Cert_Bancario";
        public string? RutDocGuid { get; set; }
        public string? RutDocKey { get; set; } = "RUT";
        public string? PeakrContractDocPath { get; set; }

     

        //External
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address Address { get; set; } = new Address();
        [NotMapped]
        public List<SelectListItem>? ProductCategories { get; set; }
        [NotMapped]
        public List<int>? SelectedCategories { get; set; }

        public virtual ICollection<CompanyCategory> Categories { get; set; }

        public virtual ICollection<Quote> Quotes { get; private set; }

        public virtual ICollection<Request> Requests { get; private set; }

        [InverseProperty("CompanySeller")]
        public ICollection<PurchaseOrder> PurchaseOrdersSells { get; private set; }

        [InverseProperty("CompanyBuyer")]
        public ICollection<PurchaseOrder> PurchaseOrdersBuys { get; private set; }
    }
}
