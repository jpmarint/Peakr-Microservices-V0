using System;
using System.Collections.Generic;

namespace SolicitudesAPI.Models
{
    public partial class Company
    {
        public Company()
        {
            Requests = new HashSet<Request>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string LogoPath { get; set; }
        public string WebSiteUrl { get; set; }
        public int? Nit { get; set; }
        public string CompanyDescription { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
