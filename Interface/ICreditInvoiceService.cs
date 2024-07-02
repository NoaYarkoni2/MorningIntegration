using MorningIntegration.Models;

namespace MorningIntegration.Interface
{
    public interface ICreditInvoiceService
    {
        Task<Document> CreateCreditInvoiceAsync(CreditInvoice creditInvoice, string id, string secret);
    }
}
