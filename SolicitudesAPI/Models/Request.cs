using System;
using System.Collections.Generic;

namespace SolicitudesAPI.Models
{
    public partial class Request
    {
        public Request()
        {
            Quotes = new HashSet<Quote>();
        }

        public int RequestId { get; set; }
        public string ProductName { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? Quantity { get; set; }
        public string RequestStatus { get; set; }
        public int? CompanyId { get; set; }
        public string PaymentConditions { get; set; }
        public string QuerySearch { get; set; }
        public int? AdressId { get; set; }
        public string RequestNotes { get; set; }

        public virtual Adress Adress { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
