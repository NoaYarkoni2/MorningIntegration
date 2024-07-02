namespace MorningIntegration.Models
{
    public class Income
    {
        public string? catalogNum { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string? currency { get; set; }
        public int? currencyRate { get; set; }
        public string? itemId { get; set; }
        public int? vatType { get; set; }
    }
}
