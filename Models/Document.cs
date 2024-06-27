using System;
using MorningIntegration.Models;

namespace MorningIntegration.Models
{
    public class Document
    {
        public string? id { get; set; }
        //public string? number { get; set; }
        public string description { get; set; }
        public string remarks { get; set; }
        public string footer { get; set; }
        public string emailContent { get; set; }
        public int type { get; set; }
        public string date { get; set; }
        public string dueDate { get; set; }
        public string lang { get; set; }
        public string currency { get; set; }
        public int vatType { get; set; }
        public Discount discount { get; set; }
        public bool rounding { get; set; }
        public bool signed { get; set; }
        public bool attachment { get; set; }
        public int maxPayments { get; set; }
        public Paymentrequestdata paymentRequestData { get; set; }
        public Client client { get; set; }
        public List<Income> income { get; set; }
        public Payment[] payment { get; set; }
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

 

    public class Income
    {
        public string catalogNum { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; } // Use decimal for price
        public string currency { get; set; }
        public int currencyRate { get; set; }
        public string itemId { get; set; }
        public int vatType { get; set; }
    }

    public class Payment
    {
        public string date { get; set; }
        public int type { get; set; }
        public decimal price { get; set; } // Use decimal for price
        public string currency { get; set; }
        public int currencyRate { get; set; }
        public string bankName { get; set; }
        public string bankBranch { get; set; }
        public string bankAccount { get; set; }
        public string chequeNum { get; set; }
        public string accountId { get; set; }
        public string transactionId { get; set; }
        public int appType { get; set; }
        public int subType { get; set; }
        public int cardType { get; set; }
        public string cardNum { get; set; }
        public int dealType { get; set; }
        public int numPayments { get; set; }
        public int firstPayment { get; set; }
    }

}
