using MorningIntegration.Interface;
using MorningIntegration.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
namespace MorningIntegration.Services
{
    public class TransactionInvoiceService : ITransactionInvoiceService
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<TransactionInvoiceService> _logger;
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public TransactionInvoiceService(IDocumentService documentService, ILogger<TransactionInvoiceService> logger, IConfiguration config, IAccountService accountService)
        {
            _documentService = documentService;
            _logger = logger;
            _config = config;
            _accountService = accountService;
        }

        public async Task<Document> CreateTransactionInvoiceAsync(TransactionInvoice transactionInvoice, string id, string secret)
        {
            var document = new Document
            {
                type = 300,
                date = transactionInvoice.date,
                dueDate = transactionInvoice.dueDate,
                lang = transactionInvoice.lang,
                currency = transactionInvoice.currency,
                client = transactionInvoice.client,
                income = transactionInvoice.income,
                description = transactionInvoice.description,
                remarks = transactionInvoice.remarks,
                footer = transactionInvoice.footer,
                emailContent= transactionInvoice.emailContent,
            };
            return await _documentService.CreateDocumentAsync(document, id, secret);
        }
    }
}
