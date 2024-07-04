using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MorningIntegration.Interface;
using MorningIntegration.Models;

namespace MorningIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditInvoiceController : ControllerBase
    {
        private readonly ICreditInvoiceService _creditInvoiceService;

        public CreditInvoiceController (ICreditInvoiceService creditInvoiceService)
        {
            _creditInvoiceService = creditInvoiceService;
        }

        [HttpPost("create-credit-invoice")]
        public async Task<IActionResult> CreateCreditInvoice([FromBody] CreditInvoice creditInvoice, string id, string secret)
        {
            try
            {
                var newCreditInvoice = await _creditInvoiceService.CreateCreditInvoiceAsync(creditInvoice, id, secret);
                return Ok(newCreditInvoice);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
