using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MorningIntegration.Services;
using MorningIntegration.Models;
using MorningIntegration.Interface;

namespace MorningIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("create-document")]
        public async Task<IActionResult> CreateDocument([FromBody] Document document)
        {
            try
            {
                var createdDocument = await _documentService.CreateDocumentAsync(document);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
