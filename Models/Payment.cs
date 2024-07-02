namespace MorningIntegration.Models
{
    public class Payment
    {
        public string date { get; set; }
        public int type { get; set; }
        public decimal price { get; set; }
        public string currency { get; set; }
        public int? currencyRate { get; set; }
        public string? bankName { get; set; }
        public string? bankBranch { get; set; }
        public string? bankAccount { get; set; }
        public string? chequeNum { get; set; }
        public string? accountId { get; set; }
        public string? transactionId { get; set; }
        public int? appType { get; set; }
        public int? subType { get; set; }
        public int? cardType { get; set; }
        public string? cardNum { get; set; }
        public int? dealType { get; set; }
        public int? numPayments { get; set; }
        public int? firstPayment { get; set; }
    }
}
