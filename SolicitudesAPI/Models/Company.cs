using System.ComponentModel.DataAnnotations;

namespace SolicitudesAPI.Models
{
    public class Company
    {

        public int CompanyId { get; set; }
        [Required]
        [Range(0,1)]
        public int CompanyType { get; set; }
        public string? ImagePath { get; set; } = "../img/BannerTemp.png";
        public string? LogoPath { get; set; } = "../img/LogoTemp.png";

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
        public string? LegalExistenceDocPath { get; set; }
        public string? BankAccountDocPath { get; set; }
        public string? RutDocPath { get; set; }
        public string? PeakrContractDocPath { get; set; }

        //External
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public List<Request> requests { get; set; }
        public List<Quote> Quotes { get; set; }

        public List<CompanyCategory> companyCategories { get; set; }

    }
}
