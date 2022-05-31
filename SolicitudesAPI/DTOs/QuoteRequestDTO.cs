using SolicitudesAPI.Models;

namespace SolicitudesAPI.DTOs
{
    public class QuoteRequestDTO
    {

        public int QuoteId { get; set; }
        public int RequestId { get; set; }        
        public QuoteDTO2 Quote { get; set; }
        public RequestModalDTO Request { get; set; }
        //public RequestDTO Request { get; set; }

        //public int RequestId { get; set; }
        //public string QuerySearch { get; set; }
        //public bool IsExactProduct { get; set; }
        //public string StatusRequest { get; set; }
        //public DateTime RequestDate { get; set; }
        //public int Quantity { get; set; }
        //public string Department { get; set; }
        //public string City { get; set; }
        //public string Line2 { get; set; }
        //public string PostalCode { get; set; }
        //public string PaymentConditions { get; set; }
        //public string Name { get; set; }
        //public string LogoPath { get; set; }
        //public string WebSiteUrl { get; set; }
        //public string ProductNeeds { get; set; }
        //public string deliveryInstructions { get; set; }      
        //public int QuoteId { get; set; }
        //public string QuoteProductName { get; set; }
        //public int DeliveryDeadLineInDays { get; set; }
        //public DateTime QuoteExpirationDate { get; set; }
        //public decimal IVA { get; set; }
        //public decimal PricePerUnit { get; set; }
        //public decimal TotalGrossPrice { get; set; }
        //public decimal TotalIVA { get; set; }
        //public decimal NetCost { get; set; }
        //public decimal TaxWithholding { get; set; }
        //public decimal ServiceCost { get; set; }
        //public decimal SellerIncome { get; set; }
        //public bool IsProductExactMatch { get; set; }
        //public string NotesToClient { get; set; }

    }
}
