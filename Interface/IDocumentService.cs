using MorningIntegration.Models;
namespace MorningIntegration.Interface
{
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(Document document, string id, string secret);
        Task<Document> GetDocumentAsync(string documentId, string id, string secret);
        Task<Document> CloseDocumentAsync(string documentId, string id, string secret);
    }
}
