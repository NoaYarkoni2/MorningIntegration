using MorningIntegration.Interface;
using MorningIntegration.Models;

namespace MorningIntegration.Services
{
    public class CreditInvoiceService : ICreditInvoiceService
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<TransactionInvoiceService> _logger;
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public CreditInvoiceService (IDocumentService documentService, ILogger<TransactionInvoiceService> logger, IConfiguration config, IAccountService accountService)
        {
            _documentService = documentService;
            _logger = logger;
            _config = config;
            _accountService = accountService;
        }

        public async Task<Document> CreateCreditInvoiceAsync(CreditInvoice creditInvoice, string id, string secret)
        {
            var document = new Document
            {
                type = 330,
                date = creditInvoice.date,
                lang = creditInvoice.lang,
                currency = creditInvoice.currency,
                client = creditInvoice.client,
                income = creditInvoice.income,
                description = creditInvoice.description,
                remarks = creditInvoice.remarks,
                footer = creditInvoice.footer,
                emailContent = creditInvoice.emailContent,
            };
            return await _documentService.CreateDocumentAsync(document, id, secret);
        }
    }
}
