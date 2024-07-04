namespace MorningIntegration.Models
{
    public class DocumentSearchResponse
    {
        public int total { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int pages { get; set; }
        public List<Document> items { get; set; }
    }
}
