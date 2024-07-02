using System;
using MorningIntegration.Models;

namespace MorningIntegration.Models
{
    public class Document
    {
        public string? id { get; set; }
        public string? description { get; set; }
        public string? remarks { get; set; }
        public string? footer { get; set; }
        public string? emailContent { get; set; }
        public int type { get; set; }
        public string date { get; set; }
        public string dueDate { get; set; }
        public string? lang { get; set; }
        public string? currency { get; set; }
        public int? vatType { get; set; }
        public Discount? discount { get; set; }
        public bool? rounding { get; set; }
        public bool? signed { get; set; }
        public bool? attachment { get; set; }
        public int? maxPayments { get; set; }
        public Paymentrequestdata? paymentRequestData { get; set; }
        public Client client { get; set; }
        public List<Income>? income { get; set; }
        public Payment[]? payment { get; set; }
    }

    public class Discount
    {
        public int amount { get; set; }
        public string type { get; set; }
    }

    public class Paymentrequestdata
    {
        public Plugin[] plugins { get; set; }
        public int maxPayments { get; set; }
    }

    public class Plugin
    {
        public string id { get; set; }
        public int group { get; set; }
        public int type { get; set; }
    }
}
