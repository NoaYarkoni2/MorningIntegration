namespace MorningIntegration.Models
{
    public class Document
    {
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string Footer { get; set; }
        public string EmailContent { get; set; }
        public int Type { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public string Lang { get; set; }
        public string Currency { get; set; }
        public int VatType { get; set; }
        public Discount Discount { get; set; }
        public bool Rounding { get; set; }
        public bool Signed { get; set; }
        public bool Attachment { get; set; }
        public int MaxPayments { get; set; }
        public Paymentrequestdata PaymentRequestData { get; set; }
        public Client Client { get; set; }
        public Income[] Income { get; set; }
        public Payment[] Payment { get; set; }
        public string[] LinkedDocumentIds { get; set; }
        public string LinkedPaymentId { get; set; }
        public string LinkType { get; set; }
    }

    public class Discount
    {
        public int Amount { get; set; }
        public string Type { get; set; }
    }

    public class Paymentrequestdata
    {
        public Plugin[] Plugins { get; set; }
        public int MaxPayments { get; set; }
    }

    public class Plugin
    {
        public string Id { get; set; }
        public int Group { get; set; }
        public int Type { get; set; }
    }

   
    public class Income
    {
        public string CatalogNum { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
        public int CurrencyRate { get; set; }
        public string ItemId { get; set; }
        public int VatType { get; set; }
    }

    public class Payment
    {
        public string Date { get; set; }
        public int Type { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
        public int CurrencyRate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankAccount { get; set; }
        public string ChequeNum { get; set; }
        public string AccountId { get; set; }
        public string TransactionId { get; set; }
        public int AppType { get; set; }
        public int SubType { get; set; }
        public int CardType { get; set; }
        public string CardNum { get; set; }
        public int DealType { get; set; }
        public int NumPayments { get; set; }
        public int FirstPayment { get; set; }


    }
}
