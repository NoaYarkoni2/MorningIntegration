using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MorningIntegration.Interface;
using MorningIntegration.Models;

namespace MorningIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxInvoiceReceiptController : ControllerBase
    {
        private readonly ITaxInvoiceReceiptService _taxInvoiceReceiptService;

        public TaxInvoiceReceiptController(ITaxInvoiceReceiptService taxInvoiceReceiptService)
        {
            _taxInvoiceReceiptService = taxInvoiceReceiptService;
        }

        [HttpPost("create-tax-invoice-receipt")]
        public async Task<IActionResult> CreateTaxInvoiceReceipt([FromBody] TaxInvoiceReceipt taxInvoiceReceipt, string id, string secret)
        {
            try
            {
                var newTaxInvoiceReceipt = await _taxInvoiceReceiptService.CreateTransactionInvoiceAsync(taxInvoiceReceipt, id, secret);
                return Ok(newTaxInvoiceReceipt);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
