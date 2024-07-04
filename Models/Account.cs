using System.ComponentModel.DataAnnotations;

namespace MorningIntegration.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? licensedDealerNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}