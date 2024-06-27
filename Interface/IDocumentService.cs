using MorningIntegration.Models;
namespace MorningIntegration.Interface
{
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(Document document);
    }
}
