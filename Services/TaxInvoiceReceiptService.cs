using MorningIntegration.Interface;
using MorningIntegration.Models;

namespace MorningIntegration.Services
{
    public class TaxInvoiceReceiptService : ITaxInvoiceReceiptService
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<TransactionInvoiceService> _logger;
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public TaxInvoiceReceiptService (IDocumentService documentService, ILogger<TransactionInvoiceService> logger, IConfiguration config, IAccountService accountService)
        {
            _documentService = documentService;
            _logger = logger;
            _config = config;
            _accountService = accountService;
        }

        public async Task<Document> CreateTransactionInvoiceAsync(TaxInvoiceReceipt taxInvoiceReceipt, string id, string secret)
        {
            var document = new Document
            {
                type = 320,
                date = taxInvoiceReceipt.date,
                lang = taxInvoiceReceipt.lang,
                currency = taxInvoiceReceipt.currency,
                client = taxInvoiceReceipt.client,
                income = taxInvoiceReceipt.income,
                payment=taxInvoiceReceipt.payment,
                description = taxInvoiceReceipt.description,
                remarks = taxInvoiceReceipt.remarks,
                footer = taxInvoiceReceipt.footer,
                emailContent = taxInvoiceReceipt.emailContent,
            };
            return await _documentService.CreateDocumentAsync(document, id, secret);
        }
    }
}
