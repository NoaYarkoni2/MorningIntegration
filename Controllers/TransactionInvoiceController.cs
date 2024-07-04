using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MorningIntegration.Services;
using MorningIntegration.Interface;
using MorningIntegration.Models;
using Microsoft.AspNetCore.Authorization;

namespace MorningIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionInvoiceController : ControllerBase
    {
        private readonly ITransactionInvoiceService _transactionInvoiceService;

        public TransactionInvoiceController(ITransactionInvoiceService transactionInvoiceService)
        {
            _transactionInvoiceService = transactionInvoiceService;
        }

        [HttpPost("create-transaction-invoice")]
        public async Task<IActionResult> CreateTransactionInvoice([FromBody] TransactionInvoice transactionInvoice, string id, string secret)
        {
            try
            {
                var newTransactionInvoice = await _transactionInvoiceService.CreateTransactionInvoiceAsync(transactionInvoice, id, secret);
                return Ok(newTransactionInvoice);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
