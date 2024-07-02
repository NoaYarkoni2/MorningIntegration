namespace MorningIntegration.Models
{
    public class Client
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public bool? active { get; set; }
        public string? department { get; set; }
        public string? taxId { get; set; }
        public string? accountingKey { get; set; }
        public int? paymentTerms { get; set; }
        public string? bankName { get; set; }
        public string? bankBranch { get; set; }
        public string? bankAccount { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? zip { get; set; }
        public string? country { get; set; }
        public int? category { get; set; }
        public int? subCategory { get; set; }
        public string? phone { get; set; }
        public string? fax { get; set; }
        public string? mobile { get; set; }
        public string? remarks { get; set; }
        public string? contactPerson { get; set; }
        public string[]? emails { get; set; }
        public string[]? labels { get; set; }
    }
}

