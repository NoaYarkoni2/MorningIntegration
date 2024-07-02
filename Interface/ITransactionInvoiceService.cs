using MorningIntegration.Models;
namespace MorningIntegration.Interface
{
    public interface ITransactionInvoiceService
    {
        Task<Document> CreateTransactionInvoiceAsync(TransactionInvoice transactionInvoice, string id, string secret);
    }
}
