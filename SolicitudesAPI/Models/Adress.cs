using System;
using System.Collections.Generic;

namespace SolicitudesAPI.Models
{
    public partial class Adress
    {
        public Adress()
        {
            Requests = new HashSet<Request>();
        }

        public int AdressId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PostalCode { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
