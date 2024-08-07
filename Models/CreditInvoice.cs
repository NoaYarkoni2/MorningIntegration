﻿namespace MorningIntegration.Models
{
    public class CreditInvoice
    {
        public const int type = 330;
        public string date { get; set; }
        public string lang { get; set; }
        public string currency { get; set; }
        public Client client { get; set; }
        public List<Income>? income { get; set; }
        public string? description { get; set; }
        public string? remarks { get; set; }
        public string? footer { get; set; }
        public string? emailContent { get; set; }
    }
}
