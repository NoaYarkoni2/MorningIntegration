using MorningIntegration.Models;

namespace MorningIntegration.Interface
{
    public interface ITaxInvoiceReceiptService
    {
        Task<Document> CreateTransactionInvoiceAsync(TaxInvoiceReceipt taxInvoiceReceipt, string id, string secret);
    }
}
