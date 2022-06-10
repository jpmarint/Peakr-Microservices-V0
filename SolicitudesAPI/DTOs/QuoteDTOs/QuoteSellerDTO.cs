﻿namespace SolicitudesAPI.DTOs.QuoteDTOs
{
    public class QuoteSellerDTO
    {

        public int QuoteId { get; set; }
        public string QuoteProductName { get; set; }
        public int DeliveryDeadLineInDays { get; set; }
        public DateTime QuoteExpirationDate { get; set; }
        public decimal NetCost { get; set; }

    }
}