namespace SolicitudesAPI.Models
{
    public class QuoteRequest
    {

            public int QuoteId { get; set; }
            public int RequestId { get; set; }
            public bool IsPurchaseOrder { get; set; } = false;
            public bool IsCancelled { get; set; } = false;
            public Quote Quote { get; set; }
            public Request Request { get; set; }

    }
}
