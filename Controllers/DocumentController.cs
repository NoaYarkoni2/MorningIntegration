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
        public async Task<IActionResult> CreateDocument([FromBody] Document document, string id, string secret)
        {
            try
            {
                var createdDocument = await _documentService.CreateDocumentAsync(document,id,secret);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-document/{documentId}")]
        public async Task<IActionResult> UpdateDocument(string documentId, string id, string secret)
        {
            try
            {
                var document = await _documentService.GetDocumentAsync(documentId,id, secret);
                return Ok(document);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("close-document")]
        public async Task<IActionResult> CloseDocument(string documentId, string id, string secret)
        {
            try
            {
                var closeDocument = await _documentService.CloseDocumentAsync(documentId, id, secret);
                return Ok(closeDocument);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
